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
            if (pageNumber < 1 || pageSize < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number and size must be greater than 0.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }

            var result = await _service.GetAll(pageNumber, pageSize, tenantId);
            var size = await _service.Count(tenantId);

            var response = new SuccessResponse<PaginatedResponseDTO<User>>(
                200,
                "Users retrieved successfully.",
                new PaginatedResponseDTO<User>(result.ToList(), size, pageNumber, pageSize)
            );

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _service.GetById(id, tenantId);
            if (result is null)
                return StatusCode(404, new ErrorResponse(404, "User not found", null));

            return Ok(new SuccessResponse<User>(200, "User found", result));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _service.GetByEmail(email);
            if (result is null)
                return StatusCode(404, new ErrorResponse(404, "User not found", null));

            return Ok(new SuccessResponse<User>(200, "User found", result));
        }

        [HttpPost("credentials")]
        public async Task<IActionResult> ValidateCredentials([FromBody] CredentialDTO credential)
        {
            try
            {
                var result = await _service.ValidateCredentials(credential);
                if (result is null)
                    return Unauthorized(new { message = "Invalid credentials." });

                return Ok(new SuccessResponse<UserLogInDTO>(200, "Login successful", result));
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
                return BadRequest();

            var currentUser = _mapper.Map<User>(user);
            currentUser.TenantId = tenantId;

            var createdUser = await _service.Create(currentUser);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdUser.Id, tenantId = tenantId },
                new SuccessResponse<User>(201, "User created", createdUser)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateUserDTO user,
            [FromQuery] Guid tenantId
        )
        {
            if (user is null)
                return BadRequest();

            var currentUser = _mapper.Map<User>(user);
            var updatedUser = await _service.Update(id, currentUser, tenantId);

            if (updatedUser is null)
                return NotFound(new ErrorResponse(404, "User not found", null));

            return Ok(new SuccessResponse<User>(200, "User updated successfully", updatedUser));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
                return NotFound(new ErrorResponse(404, "User not found", null));

            return Ok(new SuccessResponse<bool>(200, "User deleted successfully", result));
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid tenantId = default,
            [FromQuery] string search = ""
        )
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                var error = new ErrorResponse(
                    400,
                    "Page number and size must be greater than 0.",
                    null
                );
                return StatusCode(error.StatusCode, error);
            }

            var result = await _service.Search(pageNumber, pageSize, tenantId, search);
            var size = await _service.CountSearchResults(search, tenantId);

            var response = new SuccessResponse<PaginatedResponseDTO<User>>(
                200,
                "Search completed successfully.",
                new PaginatedResponseDTO<User>(result.ToList(), size, pageNumber, pageSize)
            );

            return Ok(response);
        }
    }
}
