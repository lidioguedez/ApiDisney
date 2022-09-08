using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class ApiDisneyDbContext : DbContext
    {
        public ApiDisneyDbContext(DbContextOptions<ApiDisneyDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculaPersonaje>()
                .HasKey(o => new { o.PeliculaId, o.PersonajeId });
        }

        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Personaje> Personaje { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<PeliculaPersonaje> PeliculaPersonaje { get; set; }


    }
}
