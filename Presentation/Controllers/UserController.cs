using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController
    (
        IService<User, Guid> service,
        IService<Role, int> roleService
    ) : ControllerBase
    {

        protected readonly IService<User, Guid> _service = service;
        protected readonly IService<Role, int> _roleService = roleService;

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
        public async Task<IActionResult> GetById(Guid id)
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

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            return Ok("Not implemented :c");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            var currentRole = await _roleService.GetById(user.Role);

            if (currentRole is null)
            {
                return NotFound();
            }

            var currentUser = new User
            {
                FirstNames = user.FirstNames,
                LastNames = user.LastNames,
                ShortName = user.ShortName,
                Code = user.Code,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Role = currentRole
            };

            var createdUser = await _service.Create(currentUser);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDTO user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            var currentRole = await _roleService.GetById(user.Role);

            if (currentRole is null)
            {
                return NotFound();
            }

            var currentUser = new User
            {
                FirstNames = user.FirstNames,
                LastNames = user.LastNames,
                ShortName = user.ShortName,
                Code = user.Code,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Role = currentRole
            };

            var updatedUser = await _service.Update(id, currentUser);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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