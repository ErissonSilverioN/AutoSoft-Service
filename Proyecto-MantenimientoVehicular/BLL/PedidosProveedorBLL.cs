using Microsoft.EntityFrameworkCore;
using Proyecto_MantenimientoVehicular.DAL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Proyecto_MantenimientoVehicular.BLL
{
    public class PedidosProveedorBLL
    {                                                                           
        public static bool Guardar(PedidosProveedor pedidosProveedor)
        {
            bool paso = false;
            Contexto db = new Contexto();
            
            
            

            try
            {
                if (db.pedidosProveedor.Add(pedidosProveedor) != null)
                {
                    foreach (var item in pedidosProveedor.DPedidos)
                    {
                        var proveedor = db.proveedores.Find(item.ProveedorId);

                        proveedor.CantidadPedidos += 1;
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



        public static bool Modificar(PedidosProveedor pedidosProveedor)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var anterior = PedidosProveedorBLL.Buscar(pedidosProveedor.PedidoId);

                foreach (var item in anterior.DPedidos)
                {
                    if (!pedidosProveedor.DPedidos.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;
                }

                foreach (var item in pedidosProveedor.DPedidos)
                {
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = estado;
                }




                db.Entry(pedidosProveedor).State = EntityState.Modified;
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
                var eliminar = db.pedidosProveedor.Find(id);
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

        public static PedidosProveedor Buscar(int id)
        {
            PedidosProveedor pedidosproveedor = new PedidosProveedor();
            Contexto db = new Contexto();

            try
            {
                pedidosproveedor = db.pedidosProveedor.Include(o => o.DPedidos).Where(o => o.PedidoId == id).SingleOrDefault();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return pedidosproveedor;

        }

        public static List<PedidosProveedor> GetList(Expression<Func<PedidosProveedor, bool>> pedidos)
        {
            List<PedidosProveedor> Lista = new List<PedidosProveedor>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.pedidosProveedor.Where(pedidos).ToList();
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
