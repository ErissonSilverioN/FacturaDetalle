using DetalleFactura.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DetalleFactura.NewFolder
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public string Estudiante { get; set; }
        public string Categoria { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("FacturaId")]
        public virtual List<FacturaDetalle> FacturasDetalle { get; set; } = new List<FacturaDetalle>();


        public Factura()
        {
            FacturaId = 0;
            Estudiante = string.Empty;
            Categoria = string.Empty;
            Cantidad = 0;
            Precio = 0;
            Importe = 0;
            Fecha = DateTime.Now;

        }





    }
}
