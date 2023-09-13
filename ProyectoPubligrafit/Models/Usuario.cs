using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoPubligrafit.Models
{
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }
        public int fk_rol2 { get; set; }

        public string? nombres { get; set; }
        public string? apellidos { get; set; }
        public string? email { get; set; }
        public string? contrasena { get; set; }
        public bool? estado { get; set; }

        //public virtual Rol? rol { get; set; }




    }


}
