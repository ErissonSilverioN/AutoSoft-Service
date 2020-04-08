using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_MantenimientoVehicular.Entidades
{
    public class PedidosProveedor
    {
        [Key]
        public int PedidoId { get; set; }

        public int ProveedoresId { get; set; }
        public string Categoria { get; set; }
        public string Nota { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaEntrega { get; set; }



        [ForeignKey("PedidoId")]
        public virtual List<DetallePedidos> DPedidos { get; set; } = new List<DetallePedidos>();

        public PedidosProveedor()
        {
            PedidoId = 0;
            ProveedoresId = 0;
            Categoria = string.Empty;
            Nota =  string.Empty;
            Fecha = DateTime.Now;
            FechaEntrega = DateTime.Now.AddDays(15);

            

        }
    }
}
