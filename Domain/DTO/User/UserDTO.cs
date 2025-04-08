namespace UserService.Domain.DTOs.User
{
    public class UserDTO
    {
        public string FirstNames { get; set; } = "";
        public string LastNames { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string Code { get; set; } = "";
        public int LMSId { get; set; }
        public string CI { get; set; } = "";
        public string CIType { get; set; } = "Cédula de identidad";
        public string? ImageUrl { get; set; }
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public char Gender { get; set; } = 'M';
        public DateOnly BirthDate { get; set; }
        public int Role { get; set; }
    }
}