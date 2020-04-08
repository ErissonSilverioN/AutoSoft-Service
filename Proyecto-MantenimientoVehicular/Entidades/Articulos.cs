using System;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_MantenimientoVehicular.Entidades
{
    public class Articulos
    {

        [Key]
        public int ArticuloId { get; set; }
        
        public string Articulo { get; set; }
        public string Categoria { get; set; }
        public decimal Existencia { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }



        public Articulos()
        {
            ArticuloId = 0;
            
            Articulo = string.Empty;
            Categoria = string.Empty;
            Existencia = 0;
            Precio = 0;
            Fecha = DateTime.Now;
        }
    }
}
