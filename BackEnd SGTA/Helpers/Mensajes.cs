
namespace BackEndSGTA.Helpers;

public abstract class Mensajes
{


    public abstract class MensajesClientes
    {
        //CONTROLLER
        public const string VALIDARDATOSPERSONA = "Una persona no puede tener Razón Social ni Nombre de Fantasía.";
        public const string VALIDARNOEMPRESA = "Una persona no puede ser Responsable Inscripto.";
        public const string VALIDARDNI = "Un Consumidor final solo debe tener DNI como tipo de documento.";
        public const string VALIDARDATOSEMPRESA = "Una Empresa no puede tener Nombre ni Apellido.";
        public const string VALIDARNOPERSONA = "Una empresa debe ser Responsable Inscripto.";
        public const string VALIDARCUIT = "Solo debe tener CUIT como tipo de documento.";
        public const string EXISTEDNI = "El documento es obligatorio.";
        public const string CLIENTEREPETIDO = "Ya existe un Cliente con ese documento.";
        public const string CLIENTENOENCONTRADO = "El ID del Cliente no coincide.";
        public const string CLIENTENOTFOUND = "No se encontró el Cliente con Id: ";
        public const string CLIENTEELIMINADO = "Se elimino correctamente el Cliente con Id: ";
        public const string ERRORNUMEROS = "Solo debe contener números.";
        public const string LIMITEDIGITOS = "No puede tener más de 15 dígitos.";


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
        public const int MAXQUINCE = 15;
        public const int MAXVEINTE = 20;
        public const int MAXTREINTA = 30;
        public const int MAXCUARENTA = 40;
        public const int MAXCINCUENTA = 50;
    }

    public abstract class MensajesPresupuestos
    {
        public const string PRESUPUESTONOENCONTRADO = "El ID del Presupuesto no coincide.";
        public const string PRESUPUESTONOTFOUND = "No se encontró el Presupuesto con Id: ";
        public const string PRESUPUESTOELIMINADO = "Se elimino correctamente el Presupuesto con Id: ";
        public const string TABLA_PRESUPUESTO = "presupuesto";
        public const string CAMPO_ID_PRESUPUESTO = "id_presupuesto";
        public const string TIPO_DATE = "date";
        public const string CAMPO_FECHA = "fecha";
        public const string CAMPO_MANO_DE_OBRA_CHAPA = "mano_de_obra_chapa";
        public const string CAMPO_MANO_DE_OBRA_PINTURA = "mano_de_obra_pintura";
        public const string CAMPO_TOTAL_REPUESTOS = "total_repuestos";
        public const string FECHAOBLIGATORIA = "La fecha es obligatoria.";
        public const string FECHANOFUTURA = "La fecha no puede ser futura.";
        public const string CLIENTEOBLIGATORIO = "El cliente es obligatorio.";
        public const string VALORVALIDO = "El valor debe ser mayor a 0.";
        public const int CERO = 0;


    }

    public abstract class MensajesUsuarios
    {
        public const string USUARIONOENCONTRADO = "El ID del Usuario no coincide.";
        public const string USUARIONOTFOUND = "No se encontró el Usuario con Id: ";
        public const string USUARIOELIMINADO = "Se elimino correctamente el Usuario con Id: ";
        public const string USUARIOOBLIGATORIO = "El nombre de usuario es obligatorio.";
        public const string MENSAJEUSUARIO = "El nombre de usuario no puede superar 30 caracteres.";
        public const string CORREOOBLIGATORIO = "El correo es obligatorio.";
        public const string MENSAJECORREO = "El correo no puede superar 50 caracteres.";
        public const string CORREOINVALIDO = "Correo no válido.";
        public const string CONTRASENIAOBLIGATORIO = "La contraseña es obligatoria.";
        public const string MAXCONTRASENIA = "La contraseña no puede superar 12 caracteres.";
        public const string MINCONTRASENIA = "La contraseña debe tener al menos 6 caracteres.";
        public const string MENSAJEROLUSUARIO = "El rol de usuario es inválido. Debe ser Admin, Encargado o Empleado.";
        public const string TABLA_USUARIO = "usuario";
        public const string CAMPO_ID_USUARIO = "id_usuario";
        public const string CAMPO_NOMBRE_USUARIO = "nombre_usuario";
        public const string CAMPO_CORREO = "correo";
        public const string CAMPO_ROL = "rol";
        public const string COLUMNTYPE_ENUM = "enum('Admin', 'Encargado', 'Empleado')";
        public const string CAMPO_CONTRASENIA = "contrasenia";
        public const int MINSEIS = 6;
        public const int MAXDOCE = 12;
        public const int MAXTREINTA = 30;
        public const int MAXCINCUENTA = 50;
    }

    public abstract class MensajesVehiculos
    {
        public const string VEHICULONOENCONTRADO = "El ID del Vehiculo no coincide.";
        public const string VEHICULONOTFOUND = "No se encontró el Vehiculo con Id: ";
        public const string VEHICULOELIMINADO = "Se elimino correctamente el Vehiculo con Id: ";
        public const string TABLAVEHICULO = "VEHICULO";
        public const string CAMPO_ID_VEHICULO = "id_vehiculo";
        public const string CAMPO_PATENTE = "patente";
        public const string CAMPO_MARCA = "marca";
        public const string CAMPO_MODELO = "modelo";
        public const string CAMPO_ANIO = "anio";
        public const string CAMPO_NRO_DE_CHASIS = "nro_de_chasis";
        public const string CAMPO_ESTADO = "estado";
        public const string CAMPO_FECHA_RECIBIDO = "fecha_recibido";
        public const string CAMPO_FECHA_ESPERADA = "fecha_esperada";
        public const string CAMPO_FECHA_ENTREGA = "fecha_entrega";
        public const string CAMPO_DESCRIPCION_TRABAJOS = "descripcion_trabajos";
        public const string TIPOCOLUMNAENUM = "enum('Recibido','No Recibido','Proceso','Entregado')";
        public const string TIPOCOLUMNADATE = "date";
        public const string TIPOCOLUMNATEXT = "text";
        public const string PATENTEOBLIGATORIO = "La patente es obligatoria.";
        public const string MENSAJEPATENTE = "La patente debe tener entre 6 y 10 caracteres.";
        public const string PATENTEREPETIDA = "Ya existe un Vehiculo con esa patente.";
        public const string MARCAOBLIGATORIA = "La marca es obligatoria.";
        public const string MENSAJEMARCA = "La marca no puede superar los 20 caracteres.";
        public const string MODELOOBLIGATORIO = "El modelo es obligatorio.";
        public const string MENSAJEMODELO = "El modelo no puede superar los 30 caracteres.";
        public const string ANIOMINIMO = "El año debe ser mayor a 1900.";
        public const string ANIOMAXIMO = "El año no puede ser mayor al actual";
        public const string CHASISOBLIGATORIO = "El número de chasis es obligatorio.";
        public const string MENSAJECHASIS = "El número de chasis debe tener entre 11 y 20 caracteres.";
        public const string CHASISREPETIDA = "Ya existe un Vehiculo con ese Nro de Chasis.";
        public const string ESTADOBLIGATORIO = "El estado es obligatorio.";
        public const string MENSAJEESTADO = "El estado debe ser Recibido, No Recibido, Proceso o Entregado.";
        public const string RECIBIDO = "Recibido";
        public const string NORECIBIDO = "No Recibido";
        public const string PROCESO = "Proceso";
        public const string ENTREGADO = "Entregado";
        public const string MENSAJEFECHARECIBIDO = "Debe especificar la fecha de recibido cuando el estado es 'Recibido'.";
        public const string FECHANULL = "No se puede asignar Fecha si no hay Fecha Recibido.";
        public const string MENSAJEFECHAESPERADA = "La fecha esperada no puede ser anterior a la fecha de recibido.";
        public const string FECHAENTREGAOBLIGATORIO = "Debe especificar la fecha de entrega cuando el estado es 'Entregado'.";
        public const string MENSAJEFECHAENTREGA = "La fecha de entrega no puede ser anterior a la fecha de recibido.";
        public const string MENSAJECLIENTEVALIDO = "Debe estar asociado a un cliente válido.";
        public const string CLIENTENOTFOUND = "El cliente no existe.";
        public const int MAXDIEZ = 10;
        public const int MAXQUINCE = 15;
        public const int MAXVEINTE = 20;
        public const int MAXTREINTA = 30;
    }


}