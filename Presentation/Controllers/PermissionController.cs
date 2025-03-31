using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.Permission;
using UserService.Domain.Entities.Concretes;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PermissionController
    (
        IService<Permission, int> service
    ) : ControllerBase
    {
        protected readonly IService<Permission, int> _service = service;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
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
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _service.GetByName(name);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PermissionDTO permission)
        {
            if (permission is null)
            {
                return BadRequest();
            }

            var currentPermission = new Permission
            {
                Name = permission.Name,
                Description = permission.Description
            };

            var result = await _service.Create(currentPermission);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PermissionDTO permission)
        {
            if (permission is null)
            {
                return BadRequest();
            }

            var currentPermission = new Permission
            {
                Name = permission.Name,
                Description = permission.Description
            };

            var result = await _service.Update(id, currentPermission);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}