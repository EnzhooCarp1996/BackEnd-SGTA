using BackEndSGTA.Helpers;
using BackEndSGTA.Models;
using FluentValidation;

namespace BackEndSGTA.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.NombreUsuario)
            .NotEmpty().WithMessage(Mensajes.MensajesUsuarios.USUARIOOBLIGATORIO)
            .MaximumLength(Mensajes.MensajesUsuarios.MAXTREINTA)
            .WithMessage(Mensajes.MensajesUsuarios.MENSAJEUSUARIO);

        // RuleFor(u => u.Correo)
        //     .NotEmpty().WithMessage(Mensajes.MensajesUsuarios.CORREOOBLIGATORIO)
        //     .MaximumLength(Mensajes.MensajesUsuarios.MAXCUARENTA)
        //     .WithMessage(Mensajes.MensajesUsuarios.MENSAJECORREO)
        //     .EmailAddress().WithMessage(Mensajes.MensajesUsuarios.CORREOINVALIDO);

        RuleFor(u => u.Contrasenia)
            .NotEmpty().WithMessage(Mensajes.MensajesUsuarios.CONTRASENIAOBLIGATORIO);
        // .MinimumLength(Mensajes.MensajesUsuarios.MINSEIS).WithMessage(Mensajes.MensajesUsuarios.MINCONTRASENIA);
        /*
        .Matches(@"[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
        .Matches(@"[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
        .Matches(@"\d").WithMessage("La contraseña debe contener al menos un número.");*/

        RuleFor(u => u.Role)
            .IsInEnum().WithMessage(Mensajes.MensajesUsuarios.MENSAJEROLUSUARIO);
    }
}
