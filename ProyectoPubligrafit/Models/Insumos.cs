using System.ComponentModel.DataAnnotations;

namespace ProyectoPubligrafit.Models
{
    public class Insumos
    {
        [Key]
        public int id_insumo { get; set; }
        public string nombre { get; set; }
        public float precio { get; set; }
        public int cantidad { get; set; }
        public bool? estado { get; set; }

    }
}
