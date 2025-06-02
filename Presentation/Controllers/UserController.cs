using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.Interfaces;
using UserService.Domain.DTOs.Responses;
using UserService.Domain.DTOs.User;
using UserService.Domain.Entities.Concretes;
using UserService.Presentation.Responses.Concretes;

namespace UserService.Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController(IUserService service, IMapper mapper) : ControllerBase
    {
        protected readonly IUserService _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid tenantId = default
        )
        {
            if (pageNumber < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number must be greater than or equal to 1.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }
            if (pageSize < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number must be greater than or equal to 1.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }
            var result = await _service.GetAll(pageNumber, pageSize, tenantId);
            var size = await _service.Count(tenantId);
            var response = new SuccessResponse<PaginatedResponseDTO<User>>(
                200,
                "",
                new PaginatedResponseDTO<User>(result.ToList(), size, pageNumber, pageSize)
            );
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id, Guid tenantId)
        {
            var result = await _service.GetById(id, tenantId);
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
        public async Task<IActionResult> GetAllByName(string name, Guid tenantId)
        {
            var result = await _service.GetByName(name, tenantId);
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
                return StatusCode(
                    500,
                    new { message = "An unexpected error occurred", details = ex.Message }
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromQuery] Guid tenantId,
            [FromBody] CreateUserDTO user
        )
        {
            if (user is null)
            {
                return BadRequest();
            }

            var currentUser = _mapper.Map<User>(user);
            currentUser.TenantId = tenantId;

            var createdUser = await _service.Create(currentUser);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateUserDTO user,
            Guid tenantId
        )
        {
            if (user is null)
            {
                return BadRequest();
            }

            var currentUser = _mapper.Map<User>(user);

            var updatedUser = await _service.Update(id, currentUser, tenantId);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, Guid tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid tenantId = default,
            [FromQuery] string search = ""
        )
        {
            if (pageNumber < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number must be greater than or equal to 1.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }
            if (pageSize < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number must be greater than or equal to 1.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }

            var result = await _service.Search(pageNumber, pageSize, tenantId, search);
            var size = await _service.CountSearchResults(search, tenantId);
            var response = new SuccessResponse<PaginatedResponseDTO<User>>(
                200,
                "",
                new PaginatedResponseDTO<User>(result.ToList(), size, pageNumber, pageSize)
            );
            return StatusCode(response.StatusCode, response);
        }
    }
}
