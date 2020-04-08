using Microsoft.EntityFrameworkCore;
using Proyecto_MantenimientoVehicular.DAL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Proyecto_MantenimientoVehicular.BLL
{
    public class MantenimientoBLL
    {
        public static bool Guardar(Mantenimiento mantenimiento)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.mantenimientos.Add(mantenimiento) != null)
                {
                    foreach (var item in mantenimiento.DMantenimiento)
                    {
                        var articulo = db.articulos.Find(item.ArticuloId);
                        articulo.Existencia -= item.Cantidad;
                    }

                    db.SaveChanges();
                    paso = true;
                }

                db.Dispose();
      
            }
            catch (Exception)
            {
                throw;
            }
           

            return paso;
            
        }
       

        public static bool Modificar(Mantenimiento mantenimiento)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {

                var anterior = MantenimientoBLL.Buscar(mantenimiento.MantenimientoId);

                foreach (var item in anterior.DMantenimiento)
                {
                    if (!mantenimiento.DMantenimiento.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;
                }

                foreach (var item in mantenimiento.DMantenimiento)
                {
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = estado;
                }






                db.Entry(mantenimiento).State = EntityState.Modified;
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
                var eliminar = db.mantenimientos.Find(id);
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


        public static Mantenimiento Buscar(int id)
        {
            Mantenimiento mantenimiento = new Mantenimiento();
            Contexto db = new Contexto();
            

            try
            {
                mantenimiento = db.mantenimientos.Include(o => o.DMantenimiento).Where(o => o.MantenimientoId == id).SingleOrDefault();
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return mantenimiento;
        }

        public static List<Mantenimiento> GetList(Expression<Func<Mantenimiento,bool>> mantenimiento)
        {
            List<Mantenimiento> Lista = new List<Mantenimiento>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.mantenimientos.Where(mantenimiento).ToList();
            }
            catch(Exception)
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
