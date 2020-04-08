using Microsoft.EntityFrameworkCore;
using Proyecto_MantenimientoVehicular.DAL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Proyecto_MantenimientoVehicular.BLL
{
    public class ProveedoresBLL
    {
        public static bool Guardar(Proveedores proveedores)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.proveedores.Add(proveedores) != null)
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

        public static bool Modificar(Proveedores proveedores)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(proveedores).State = EntityState.Modified;
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
                var eliminar = db.proveedores.Find(id);
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


        public static Proveedores Buscar(int id)
        {

            Contexto db = new Contexto();
            Proveedores proveedores = new Proveedores();

            try
            {
                proveedores = db.proveedores.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return proveedores;
        }

        public static List<Proveedores> GetList(Expression<Func<Proveedores, bool>> proveedores)
        {
            List<Proveedores> Lista = new List<Proveedores>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.proveedores.Where(proveedores).ToList();
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
