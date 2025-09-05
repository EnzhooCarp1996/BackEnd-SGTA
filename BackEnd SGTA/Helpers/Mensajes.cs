
namespace BackEndSGTA.Helpers;

public abstract class Mensajes
{
    public const string ERRORNUMEROS = "Solo debe contener números.";
    public const string LIMITEDIGITOS = "No puede tener más de 15 dígitos.";
    public const string CORREOINVALIDO = "Correo no válido.";
    public const int MAXDIEZ = 10;
    public const int MAXQUINCE = 15;
    public const int MAXVEINTE = 20;
    public const int MAXTREINTA = 30;
    public const int MAXCUARENTA = 40;
    public const int MAX150 = 150;
    public const int QUINCE = 15;

    public abstract class MensajesClientes
    {
        //CONTROLLER
        public const string VALIDARPERSONA = "Una persona no puede tener Razón Social ni Nombre de Fantasía.";
        public const string VALIDARDNI = "Una persona debe tener DNI como tipo de documento.";
        public const string VALIDAREMPRESA = "Una empresa no puede tener Nombre ni Apellido.";
        public const string VALIDARCUIT = "Una Empresa o Monotributista debe tener CUIT como tipo de documento.";
        public const string CLIENTEREPETIDO = "Ya existe un Cliente con ese documento..";
        public const string CLIENTENOTFOUND = "No se encontró un Cliente con Id: ";
        public const string CLIENTEOK = "Eliminado correctamente el Cliente con Id: ";


        //CONFIGURATION
        //CAMPOS DE TABLAS
        public const string TABLACLIENTE = "cliente";
        public const string CAMPO_ID_CLIENTE = "id_cliente";
        public const string CAMPO_TIPO_CLIENTE = "tipo_cliente";
        public const string CAMPO_TELEFONO = "telefono";
        public const string CAMPO_CELULAR = "celular";
        public const string CAMPO_RESPONSABILIDAD = "responsabilidad";
        public const string CAMPO_TIPO_DOCUMENTO = "tipo_documento";
        public const string CAMPO_DOCUMENTO = "documento";
        public const string CAMPO_NOMBRE = "nombre";
        public const string CAMPO_APELLIDO = "apellido";
        public const string CAMPO_RAZON_SOCIAL = "razon_social";
        public const string CAMPO_NOMBRE_DE_FANTASIA = "nombre_de_fantasia";
        public const int MAXVEINTE = 20;
        public const int MAXTREINTA = 30;
        public const int MAXCUARENTA = 40;
        public const int MAXCINCUENTA = 40;
    }
}