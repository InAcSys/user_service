namespace UserService.Domain.DTOs.User
{
    public class UserCredentialsDTO
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] Salt { get; set; } = [];
    }
}