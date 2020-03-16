using DetalleFactura.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using DetalleFactura.Entidades;
using DetalleFactura.NewFolder;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DetalleFactura.BLL
{
    public class FacturaBLL
    {
        public static bool Guardar(Factura factura)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.facturas.Add(factura) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }


        public static bool Modificar(Factura factura)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM FacturaDetalle Where FacturaId = {factura.FacturaId}");

                foreach ( var item in factura.FacturasDetalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }


                db.Entry(factura).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {

            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.facturas.Find(id);
                db.Entry(eliminar).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;

        }

        public  static Factura  Buscar(int id)
        {

            Factura factura = new Factura();
            Contexto db = new Contexto();

            try
            {
                factura = db.facturas.Include(o => o.FacturasDetalle).Where(o => o.FacturaId == id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return factura;

        }
    }
}
