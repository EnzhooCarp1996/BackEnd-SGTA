using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndSGTA.Models;

public class Factura
{
    public int IdFactura { get; set; }
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime Fecha { get; set; }

    [Column("mano_de_obra_chapa")]
    public decimal ManoDeObraChapa { get; set; }

    [Column("mano_de_obra_pintura")]
    public decimal ManoDeObraPintura { get; set; }

    [Column("total_repuestos")]
    public decimal TotalRepuestos { get; set; }

    public int IdCliente { get; set; }

    [ForeignKey("IdCliente")]
    public Cliente? Cliente { get; set; }




}

/* 
    id_factura INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATE NOT NULL,
    mano_de_obra_chapa DECIMAL(10,2) NOT NULL,
    mano_de_obra_pintura DECIMAL(10,2),
    total_repuestos DECIMAL(10,2),
    id_cliente INT NOT NULL
*/