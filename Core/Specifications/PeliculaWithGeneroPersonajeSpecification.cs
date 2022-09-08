using Core.DTO;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class PeliculaWithGeneroPersonajeSpecification : BaseSpecification<Pelicula>
    {
        public PeliculaWithGeneroPersonajeSpecification(string? name, int? genre, string? order)
            : base(x => (string.IsNullOrEmpty(name) || x.Titulo.Contains(name)) &&
                        (!genre.HasValue || x.GeneroId == genre)
        )
        {
            AddInclude(g => g.Genero);
            AddInclude($"{nameof(Pelicula.Personajes)}.{nameof(PeliculaPersonaje.Personaje)}");

            if (!string.IsNullOrEmpty(order))
            {
                switch (order.ToUpper())
                {
                    case "ASC":
                        AddOrderBy(p => p.Titulo);
                        break;
                    case "DESC":
                        AddOrderByDesc(p => p.Titulo);
                        break;
                    default:
                        AddOrderBy(p => p.PeliculaId);
                        break;
                }
            }


        }

        public PeliculaWithGeneroPersonajeSpecification(int id) : base(x => x.PeliculaId == id)
        {
            AddInclude(g => g.Genero);
            AddInclude($"{nameof(Pelicula.Personajes)}.{nameof(PeliculaPersonaje.Personaje)}");

        }

    }
}
