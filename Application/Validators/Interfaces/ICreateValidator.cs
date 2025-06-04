using FluentValidation;

namespace UserService.Application.Validators.Interfaces
{
    public interface ICreateValidator<in T> : IValidator<T> { }
}
