using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPubligrafit.Models
{
    public class Rol
    {
        

        [Key]
        
        public int id_rol { get; set; }
        
        public string? nombre_rol { get; set; }
        public DateTime fecha { get; set; }

        //public virtual ICollection<Usuario> Usuario { get; set; }



    }
}
