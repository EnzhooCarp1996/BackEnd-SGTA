using static BackEndSGTA.Models.Cliente;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using FluentValidation;
using BackEndSGTA.Data;

public class ClienteValidator : AbstractValidator<Cliente>
{
    private readonly AppDbContext _context;

    public ClienteValidator(AppDbContext context)
    {
        _context = context;

        RuleFor(c => c.Telefono)
            .Matches(@"^\d+$").WithMessage(Mensajes.MensajesClientes.ERRORNUMEROS)
            .MaximumLength(Mensajes.MensajesClientes.MAXQUINCE).WithMessage(Mensajes.MensajesClientes.LIMITEDIGITOS);

        RuleFor(c => c.Celular)
            .Matches(@"^\d+$").WithMessage(Mensajes.MensajesClientes.ERRORNUMEROS)
            .MaximumLength(Mensajes.MensajesClientes.MAXQUINCE).WithMessage(Mensajes.MensajesClientes.LIMITEDIGITOS);

        RuleFor(c => c.Documento)
            .Matches(@"^\d+$").WithMessage(Mensajes.MensajesClientes.ERRORNUMEROS)
            .MaximumLength(Mensajes.MensajesClientes.MAXVEINTE).WithMessage(Mensajes.MensajesClientes.LIMITEDIGITOS);

        // Validación de duplicado en documento
        RuleFor(c => c.Documento)
            .NotEmpty().WithMessage(Mensajes.MensajesClientes.EXISTEDNI)
            .Must((cliente, documento) => !DocumentoDuplicado(cliente))
            .WithMessage(Mensajes.MensajesClientes.CLIENTEREPETIDO);

        // Persona vs Empresa
        When(c => c.TipoCliente == TipoDeCliente.Persona, () =>
        {
            RuleFor(c => c.RazonSocial)
                .Empty().WithMessage(Mensajes.MensajesClientes.VALIDARDATOSPERSONA);

            RuleFor(c => c.NombreDeFantasia)
                .Empty().WithMessage(Mensajes.MensajesClientes.VALIDARDATOSPERSONA);

            RuleFor(c => c.TipoDocumento)
                .Must(tipo => tipo == TipoDeDocumento.DNI || tipo == TipoDeDocumento.CUIT)
                .WithMessage(cliente => cliente.Responsabilidad switch
                {
                    TipoResponsabilidad.ConsumidorFinal => Mensajes.MensajesClientes.VALIDARDNI,
                    TipoResponsabilidad.Monotributista => Mensajes.MensajesClientes.VALIDARCUIT,
                    _ => ""
                });
        });

        When(c => c.TipoCliente == TipoDeCliente.Empresa, () =>
        {
            RuleFor(c => c.Nombre)
                .Empty().WithMessage(Mensajes.MensajesClientes.VALIDARDATOSEMPRESA);

            RuleFor(c => c.Apellido)
                .Empty().WithMessage(Mensajes.MensajesClientes.VALIDARDATOSEMPRESA);

            RuleFor(c => c.TipoDocumento)
                .Equal(TipoDeDocumento.CUIT)
                .WithMessage(Mensajes.MensajesClientes.VALIDARCUIT);

            RuleFor(c => c.Responsabilidad)
                .Equal(TipoResponsabilidad.ResponsableInscripto)
                .WithMessage(Mensajes.MensajesClientes.VALIDARNOPERSONA);
        });
    }

    private bool DocumentoDuplicado(Cliente cliente)
    {
        // Ignorar el mismo cliente en PUT
        return _context.Clientes.Any(c => c.Documento == cliente.Documento && c.IdCliente != cliente.IdCliente);
    }
}
