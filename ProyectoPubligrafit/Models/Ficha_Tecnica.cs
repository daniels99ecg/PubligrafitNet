using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ProyectoPubligrafit.Models
{
    public class Ficha_Tecnica
    {
        [Key]
        public int id_ft { get; set; }

        public int fk_insumo { get; set; }
        public int cantidad_insumo { get; set; }
        public float costo_insumo { get; set; }
        public float costo_final_producto { get; set; }
        public string? detalle { get; set; }
        public bool? estado { get; set; }

    }
}
