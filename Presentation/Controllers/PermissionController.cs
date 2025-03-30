using Microsoft.AspNetCore.Mvc;
using UserService.Domain.DTOs.Permission;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok("Get all ok :D");
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetAllByName(string name)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDTO permission)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePermissionDTO permission)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}