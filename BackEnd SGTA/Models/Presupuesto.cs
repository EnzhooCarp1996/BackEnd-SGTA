using System.Text.Json.Serialization;

namespace BackEndSGTA.Models;

public class Presupuesto
{
    public int IdPresupuesto { get; set; }
    public DateTime Fecha { get; set; }
    public decimal ManoDeObraChapa { get; set; }
    public decimal ManoDeObraPintura { get; set; }
    public decimal TotalRepuestos { get; set; }
    public int IdCliente { get; set; }

    [JsonIgnore]
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