using UserService.Domain.Entities.Abstracts;

namespace UserService.Domain.Entities.Concretes
{
    public class User : Entity<Guid>
    {
        public string FirstNames { get; set; } = "";
        public string LastNames { get; set; } = "";
        public string ShortName { get; set; } = "";
        public string Code { get; set; } = "";
        public int LMSId { get; set; }
        public string? ImageUrl { get; set; }
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public char Gender { get; set; } = 'M';
        public DateTime BirthDate { get; set; }
        public Role? Role { get; set; }
    }
}