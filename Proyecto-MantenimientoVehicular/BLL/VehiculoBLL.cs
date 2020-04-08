using Microsoft.EntityFrameworkCore;
using Proyecto_MantenimientoVehicular.DAL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Proyecto_MantenimientoVehicular.BLL
{
    public class VehiculoBLL
    {
        public static bool Guardar(Vehiculos vehiculos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.vehiculos.Add(vehiculos) != null)
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

        public static bool Modificar(Vehiculos vehiculos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(vehiculos).State = EntityState.Modified;
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
                var eliminar = db.vehiculos.Find(id);
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


        public static Vehiculos Buscar(int id)
        {

            Contexto db = new Contexto();
            Vehiculos vehiculos = new Vehiculos();

            try
            {
                vehiculos = db.vehiculos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return vehiculos;
        }

        public static List<Vehiculos> GetList(Expression<Func<Vehiculos, bool>> vehiculos)
        {
            List<Vehiculos> Lista = new List<Vehiculos>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.vehiculos.Where(vehiculos).ToList();
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

    }
}
