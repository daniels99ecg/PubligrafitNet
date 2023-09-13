using Microsoft.EntityFrameworkCore;
using ProyectoPubligrafit.Models;
using System.Collections.Generic;

namespace ProyectoPubligrafit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //Instancioamos el modelo Libros

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }

		public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Insumos> Insumos { get; set; }

        public DbSet<Ficha_Tecnica> Ficha_Tecnica { get; set; }

        public DbSet<Compras> Compras { get; set; }



    }

}
