using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using FluentValidation;
using BackEndSGTA.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEndSGTA.Validators;

public class PresupuestoValidator : AbstractValidator<Presupuesto>
{
    private readonly AppDbContext _context;

    public PresupuestoValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(p => p.Fecha)
            .NotEmpty().WithMessage(Mensajes.MensajesPresupuestos.FECHAOBLIGATORIA)
            .LessThanOrEqualTo(DateTime.Today).WithMessage(Mensajes.MensajesPresupuestos.FECHANOFUTURA);

        RuleFor(p => p.ManoDeObraChapa)
            .GreaterThan(Mensajes.MensajesPresupuestos.CERO).WithMessage(Mensajes.MensajesPresupuestos.VALORVALIDO);

        RuleFor(p => p.ManoDeObraPintura)
            .GreaterThanOrEqualTo(Mensajes.MensajesPresupuestos.CERO).WithMessage(Mensajes.MensajesPresupuestos.VALORVALIDO);

        RuleFor(p => p.TotalRepuestos)
            .GreaterThanOrEqualTo(Mensajes.MensajesPresupuestos.CERO).WithMessage(Mensajes.MensajesPresupuestos.VALORVALIDO);

        // Cliente relacionado: obligatorio
        RuleFor(p => p.IdCliente)
            .NotEmpty().WithMessage(Mensajes.MensajesPresupuestos.CLIENTEOBLIGATORIO)
            .GreaterThan(0).WithMessage(Mensajes.MensajesVehiculos.MENSAJECLIENTEVALIDO)
            .MustAsync(async (id, cancellation) =>
                await _context.Clientes.AnyAsync(c => c.IdCliente == id, cancellation))
            .WithMessage(Mensajes.MensajesVehiculos.CLIENTENOTFOUND);
    }
}

