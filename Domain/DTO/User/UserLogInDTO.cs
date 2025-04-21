namespace UserService.Domain.DTOs.User
{
    public class UserLogInDTO
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
    }
}