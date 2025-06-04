using FluentValidation;

namespace UserService.Application.Validators.Interfaces
{
    public interface IUpdateValidator<in T> : IValidator<T> { }
}
