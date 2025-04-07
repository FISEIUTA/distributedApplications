using ApiResD.Models;
using FluentValidation;

namespace WebApiPerson.Validators
{
    public class ClienteValidator : AbstractValidator<Client>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(c => c.email).NotEmpty().EmailAddress().WithMessage("El correo electrónico es obligatorio y debe ser válido.");
        }
    }
}