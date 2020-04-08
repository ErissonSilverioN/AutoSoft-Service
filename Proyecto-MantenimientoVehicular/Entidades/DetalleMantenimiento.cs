using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using System;


namespace Proyecto_MantenimientoVehicular.Entidades
{
    public class DetalleMantenimiento
    {
        [Key]
        public int Id { get; set; }
        public int MantenimientoId { get; set; }
        public int ArticuloId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }


 

        public DetalleMantenimiento()
        {
            
        }

        public DetalleMantenimiento(int id, int mantenimientoId, int articuloId, string descripcion, int cantidad, decimal precio, decimal importe)
        {
            Id = id;
            MantenimientoId = mantenimientoId;
            ArticuloId = articuloId;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }

       
    }
}
