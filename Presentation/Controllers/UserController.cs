using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController
    (
        IUserService service
    ) : ControllerBase
    {

        protected readonly IUserService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }
            if (pageSize < 1)
            {
                return BadRequest("Page size must be greater than or equal to 1.");
            }
            var result = await _service.GetAll(pageNumber, pageSize);
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

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _service.GetByEmail(email);
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

        [HttpPost("credentials")]
        public async Task<IActionResult> ValidateCredentials([FromBody] CredentialDTO credential)
        {
            try
            {
                var result = await _service.ValidateCredentials(credential);
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (InvalidDataException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            var currentUser = new User
            {
                FirstNames = user.FirstNames,
                LastNames = user.LastNames,
                ShortName = user.ShortName,
                Code = user.Code,
                CI = user.CI,
                CIType = user.CIType,
                ImageUrl = user.ImageUrl,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                RoleId = user.Role
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

            var currentUser = new User
            {
                FirstNames = user.FirstNames,
                LastNames = user.LastNames,
                ShortName = user.ShortName,
                Code = user.Code,
                LMSId = user.LMSId,
                CI = user.CI,
                CIType = user.CIType,
                ImageUrl = user.ImageUrl,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                RoleId = user.Role
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