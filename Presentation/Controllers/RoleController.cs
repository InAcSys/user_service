using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.Role;
using UserService.Domain.Entities.Concretes;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class RoleController(
        IService<Role, int> service,
        IService<RolePermission, int> rolePermissionService
    ) : ControllerBase
    {
        protected readonly IService<Role, int> _service = service;
        protected readonly IService<RolePermission, int> _rolePermissionService = rolePermissionService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAllByName(string name)
        {
            var result = await _service.GetByName(name);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDTO role)
        {
            if (role is null)
            {
                return BadRequest();
            }

            var existingRole = await _service.GetByName(role.Name);
            if (existingRole is not null)
            {
                return Conflict();
            }

            var createRole = new Role { Name = role.Name };
            var result = await _service.Create(createRole);
            if (result is null)
            {
                return BadRequest("Error al crear el rol.");
            }
            if (!role.PermissionIds.Any())
            {
                return BadRequest();
            }
            var rolePermissions = role.PermissionIds.Select(permissionId => new RolePermission
            {
                RoleId = result.Id,
                PermissionId = permissionId
            }).ToList();

            foreach (var rolePermission in rolePermissions)
            {
                await _rolePermissionService.Create(rolePermission);
            }

            var createdRole = await _service.GetById(result.Id);
            if (createdRole is null)
            {
                return BadRequest("Error al recuperar el rol creado.");
            }

            var createdRoleDTO = new CreateRoleDTO
            {
                Id = createdRole.Id,
                Name = createdRole.Name,
                PermissionIds = createdRole.RolePermissions.Select(rp => rp.PermissionId).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = createdRole.Id }, createdRoleDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RoleDTO role)
        {
            if (role is null)
            {
                return BadRequest();
            }
            var existingRole = await _service.GetById(id);
            if (existingRole is null)
            {
                return NotFound();
            }
            var updatedRole = new Role
            {
                Name = role.Name
            };
            var result = await _service.Update(id, updatedRole);
            if (result is null)
            {
                return BadRequest();
            }
            var existingRolePermissions = await _rolePermissionService.GetAll();
            foreach (var permissionId in role.PermissionIds)
            {
                var existingRolePermission = existingRolePermissions.FirstOrDefault(rp => rp.RoleId == id && rp.PermissionId == permissionId);
                if (existingRolePermission is null)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = id,
                        PermissionId = permissionId
                    };
                    await _rolePermissionService.Create(rolePermission);
                }
            }
            foreach (var existingRolePermission in existingRolePermissions)
            {
                if (existingRolePermission.RoleId == id && !role.PermissionIds.Contains(existingRolePermission.PermissionId))
                {
                    await _rolePermissionService.Delete(existingRolePermission.Id);
                }
            }
            var updatedRoleDTO = new CreateRoleDTO
            {
                Id = existingRole.Id,
                Name = existingRole.Name,
                PermissionIds = existingRole.RolePermissions.Select(rp => rp.PermissionId).ToList()
            };
            return CreatedAtAction(nameof(GetById), new { id = existingRole.Id }, updatedRoleDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}