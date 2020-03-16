using DetalleFactura.NewFolder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DetalleFactura.DAL
{
   public  class Contexto : DbContext
    {
        public DbSet<Factura> facturas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source = Factura.db");

        }
    }
}
