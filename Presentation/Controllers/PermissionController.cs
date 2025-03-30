using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.Permission;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PermissionController
    (
        IService<PermissionDTO, int> service
    ) : ControllerBase
    {
        protected readonly IService<PermissionDTO, int> _service = service;
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

            var result = await _service.Create(permission);
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

            var result = await _service.Update(id, permission);
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