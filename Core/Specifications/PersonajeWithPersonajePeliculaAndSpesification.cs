using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class PersonajeWithPersonajePeliculaAndSpesification : BaseSpecification<Personaje>
    {
        public PersonajeWithPersonajePeliculaAndSpesification(string? name, int? age, int? movieId) 
            : base (n => (string.IsNullOrEmpty(name) || n.Nombre.Contains(name)) && 
                         (!age.HasValue || n.Edad == age) && (!movieId.HasValue || n.Peliculas.Any(p => p.PeliculaId == movieId))
            ) 
        {
           
        }

        public PersonajeWithPersonajePeliculaAndSpesification(int id) : base (p => p.PersonajeId == id)
        {
            AddInclude(x => x.Peliculas);
            AddInclude($"{nameof(Personaje.Peliculas)}.{nameof(PeliculaPersonaje.Pelicula)}");
        }
    }


}
