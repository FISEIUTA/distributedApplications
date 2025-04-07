using ApiResD.Models;
using FluentValidation;

namespace WebApiPerson.Validators
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(p => p.Product).NotEmpty().WithMessage("El producto es obligatorio.");
            RuleFor(p => p.Amount).GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0.");
        }
    }
}