using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_MantenimientoVehicular.Entidades
{
    public class Mantenimiento
    {

        [Key]
        public int MantenimientoId { get; set; }
        public DateTime ProximoMantemiento { get; set; }
        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }
        public string Servicios { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Itebis { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }

        

       
        

        [ForeignKey("MantenimientoId")]
        public virtual List<DetalleMantenimiento> DMantenimiento { get; set; } = new List<DetalleMantenimiento>();

        public Mantenimiento()
        {
            MantenimientoId = 0;
            ProximoMantemiento = DateTime.Now.AddMonths(3);
            VehiculoId = 0;
            ClienteId = 0;
            SubTotal = 0;
            Itebis = 0;
            Total = 0;
            Fecha = DateTime.Now;


        }
    }
}
