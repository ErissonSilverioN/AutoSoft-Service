using Microsoft.EntityFrameworkCore;
using Proyecto_MantenimientoVehicular.DAL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Proyecto_MantenimientoVehicular.BLL
{
    public class ArticuloBLL
    {
        public static bool Guardar(Articulos articulos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.articulos.Add(articulos) != null)
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

        public static bool Modificar(Articulos articulos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(articulos).State = EntityState.Modified;
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
                var eliminar = db.articulos.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
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


        public static Articulos Buscar(int id)
        {

            Contexto db = new Contexto();
            Articulos articulos = new Articulos();

            try
            {
                articulos = db.articulos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return articulos;
        }

        public static List<Articulos> GetList(Expression<Func<Articulos, bool>> articulos)
        {
            List<Articulos> Lista = new List<Articulos>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.articulos.Where(articulos).ToList();
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;

        }

        public static Decimal CalcularItbis(decimal subtotal)
        {
            return subtotal * (decimal)0.18;
        }

        public static Decimal CalcularImporte(decimal cantidad, decimal precio)
        {
            decimal Importe;

            Importe = cantidad * precio;


            return Importe;
        }

        public static Decimal CalcularSubtotal(decimal importe)
        {
            return importe;
        }


        public static Decimal CalcularTotal(decimal subtotal, decimal itbis)
        {   

            return subtotal + itbis;
        }
    }
}
