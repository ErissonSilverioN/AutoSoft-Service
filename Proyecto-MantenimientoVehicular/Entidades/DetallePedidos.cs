using System.ComponentModel.DataAnnotations;

namespace Proyecto_MantenimientoVehicular.Entidades
{
    public class DetallePedidos
    {
        
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        
        public int ProveedorId { get; set; }
        public string Categoria { get; set; }
        public string Articulo { get; set; }
        public int Unidad { get; set; }
        public decimal Precio { get; set; }





        public DetallePedidos()
        {

        }

        public DetallePedidos(int id, int pedidoId, int proveedor,string categoria, string articulo, int unidad, decimal precio)
        {
            Id = id;
            PedidoId = pedidoId;
            ProveedorId = proveedor;
            Categoria = categoria;
            Articulo = articulo;
            Unidad = unidad;
            Precio = precio;
        }
    }
}
