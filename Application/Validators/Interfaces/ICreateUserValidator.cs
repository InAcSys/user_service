using UserService.Domain.Entities.Concretes;

namespace UserService.Application.Validators.Interfaces
{
    public interface ICreateUserValidator : ICreateValidator<User> { }
}
