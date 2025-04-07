using ApiResD.Models;
using FluentValidation;

namespace WebApiPerson.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(p => p.Age).GreaterThan(0).WithMessage("La edad debe ser mayor que 0.");
        }
    }
}