using System;
using System.Collections.Generic;
using System.Text;

namespace DetalleFactura.Entidades
{
   public  class FacturaDetalle
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int EstudianteId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public FacturaDetalle()
        {

        }

        public FacturaDetalle(int id, int facturaId, int estudianteId, decimal cantidad, decimal precio, decimal importe)
        {
            Id = id;
            FacturaId = facturaId;
            EstudianteId = estudianteId;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }
}
