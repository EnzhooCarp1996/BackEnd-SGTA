namespace BackEndSGTA.Validators;

using BackEndSGTA.Models;
using System.Linq;

public static class PartesVehiculoValidator
{
    // Verifica si el componente ya existe en la categoría
    public static bool ComponenteExiste(PartesVehiculo categoria, string componente)
    {
        return categoria.Componentes
            .Any(c => c.Nombre.Trim().ToLower() == componente.Trim().ToLower());
    }

    // validar nombre de categoría y componente
    public static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.");
    }
}
