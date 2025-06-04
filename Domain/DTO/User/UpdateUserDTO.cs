namespace UserService.Domain.DTOs.User
{
    public class UpdateUserDTO
    {
        public string FirstNames { get; set; } = "";
        public string LastNames { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string CI { get; set; } = "";
        public string CIType { get; set; } = "";
        public string? ImageUrl { get; set; } = "";
        public string? Address { get; set; } = "";
        public string? PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public char Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public int RoleId { get; set; }
    }
}
