using Microsoft.EntityFrameworkCore;
using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using FluentValidation;
using BackEndSGTA.Data;

namespace BackEndSGTA.Validators;

public class VehiculoValidator : AbstractValidator<Vehiculo>
{
    private readonly AppDbContext _context;

    public VehiculoValidator(AppDbContext context)
    {
        _context = context;

        // Patente: obligatoria, longitud máxima y unica
        RuleFor(v => v.Patente)
            .NotEmpty().WithMessage(Mensajes.MensajesVehiculos.PATENTEOBLIGATORIO)
            .Length(6, Mensajes.MensajesVehiculos.MAXDIEZ)
            .WithMessage(Mensajes.MensajesVehiculos.MENSAJEPATENTE)
            .Must((Vehiculo, Patente) => !PatenteDuplicada(Vehiculo))
            .WithMessage(Mensajes.MensajesVehiculos.PATENTEREPETIDA);

        // Marca: obligatoria y longitud máxima
        RuleFor(v => v.Marca)
            .NotEmpty().WithMessage(Mensajes.MensajesVehiculos.MARCAOBLIGATORIA)
            .MaximumLength(Mensajes.MensajesVehiculos.MAXVEINTE).WithMessage(Mensajes.MensajesVehiculos.MENSAJEMARCA);

        // Modelo: obligatorio y longitud máxima
        RuleFor(v => v.Modelo)
            .NotEmpty().WithMessage(Mensajes.MensajesVehiculos.MODELOOBLIGATORIO)
            .MaximumLength(Mensajes.MensajesVehiculos.MAXTREINTA).WithMessage(Mensajes.MensajesVehiculos.MENSAJEMODELO);

        // Año: debe ser razonable
        RuleFor(v => v.Anio)
            .GreaterThan(1900).WithMessage(Mensajes.MensajesVehiculos.ANIOMINIMO)
            .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage(Mensajes.MensajesVehiculos.ANIOMAXIMO);

        // Número de chasis: obligatorio, longitud fija/restringida
        RuleFor(v => v.NroDeChasis)
            .NotEmpty().WithMessage(Mensajes.MensajesVehiculos.CHASISOBLIGATORIO)
            .Length(11, 20).WithMessage(Mensajes.MensajesVehiculos.MENSAJECHASIS)
            .Must((Vehiculo, NroDeChasis) => !ChasisDuplicado(Vehiculo))
            .WithMessage(Mensajes.MensajesVehiculos.CHASISREPETIDA);

        // Estado, valores esperados
        RuleFor(v => v.Estado)
            .NotEmpty().WithMessage(Mensajes.MensajesVehiculos.ESTADOBLIGATORIO)
            .Must(e => new[] { Mensajes.MensajesVehiculos.RECIBIDO, Mensajes.MensajesVehiculos.NORECIBIDO, Mensajes.MensajesVehiculos.PROCESO, Mensajes.MensajesVehiculos.ENTREGADO }
            .Contains(e))
            .WithMessage(Mensajes.MensajesVehiculos.MENSAJEESTADO);

        //condicion de fecha_recibido con estado recibido
        RuleFor(v => v.FechaRecibido)
            .NotNull()
            .When(v => v.Estado != null && v.Estado.Equals(Mensajes.MensajesVehiculos.RECIBIDO, StringComparison.OrdinalIgnoreCase))
            .WithMessage(Mensajes.MensajesVehiculos.MENSAJEFECHARECIBIDO);

        // No se puede setear FechaEsperada si no hay FechaRecibido
        RuleFor(v => v.FechaEsperada)
            .Empty()
            .When(v => !v.FechaRecibido.HasValue)
            .WithMessage(Mensajes.MensajesVehiculos.FECHANULL);

        // No se puede setear FechaEntrega si no hay FechaRecibido
        RuleFor(v => v.FechaEntrega)
            .Empty()
            .When(v => !v.FechaRecibido.HasValue)
            .WithMessage(Mensajes.MensajesVehiculos.FECHANULL);

        // Fechas: validaciones consistentes
        RuleFor(v => v.FechaEsperada)
            .GreaterThanOrEqualTo(v => v.FechaRecibido)
            .When(v => v.FechaRecibido.HasValue && v.FechaEsperada.HasValue)
            .WithMessage(Mensajes.MensajesVehiculos.MENSAJEFECHAESPERADA);

        // Estado = Entregado -> FechaEntrega obligatoria y >= FechaRecibido
        RuleFor(v => v.FechaEntrega)
            .NotNull()
            .When(v => v.Estado != null && v.Estado.Equals(Mensajes.MensajesVehiculos.ENTREGADO, StringComparison.OrdinalIgnoreCase))
            .WithMessage(Mensajes.MensajesVehiculos.FECHAENTREGAOBLIGATORIO)
            .GreaterThanOrEqualTo(v => v.FechaRecibido)
            .When(v => v.FechaRecibido.HasValue)
            .WithMessage(Mensajes.MensajesVehiculos.MENSAJEFECHAENTREGA);


        // Cliente relacionado: obligatorio
        RuleFor(p => p.IdCliente)
            .NotEmpty().WithMessage(Mensajes.MensajesPresupuestos.CLIENTEOBLIGATORIO)
            .GreaterThan(0).WithMessage(Mensajes.MensajesVehiculos.MENSAJECLIENTEVALIDO)
            .MustAsync(async (id, cancellation) =>
                await _context.Clientes.AnyAsync(c => c.IdCliente == id, cancellation))
            .WithMessage(Mensajes.MensajesVehiculos.CLIENTENOTFOUND);
    }

    private bool PatenteDuplicada(Vehiculo vehiculo)
    {
        return _context.Vehiculos.Any(v =>
            v.Patente == vehiculo.Patente &&
            v.IdVehiculo != vehiculo.IdVehiculo); // ignorar el mismo en PUT
    }

    private bool ChasisDuplicado(Vehiculo vehiculo)
    {
        return _context.Vehiculos.Any(v =>
            v.NroDeChasis == vehiculo.NroDeChasis &&
            v.IdVehiculo != vehiculo.IdVehiculo); // ignorar el mismo en PUT
    }

}
