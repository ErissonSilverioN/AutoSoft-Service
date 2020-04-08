using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_MantenimientoVehicular.Entidades
{
    public class EntradaArticulos
    {
        [Key]
        public int EntradaArticuloId { get; set; }
        public int ArticuloId { get; set; }
        
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }

        public EntradaArticulos()
        {
            EntradaArticuloId = 0;
            ArticuloId = 0;
           
            Cantidad = 0;
            Fecha = DateTime.Now;
        }
    }
}
