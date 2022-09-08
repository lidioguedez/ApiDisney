using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public  class Pelicula
    {
        public int PeliculaId { get; set; }
        public string Titulo { get; set; }
        public string? Imagen { get; set; }
        [MaxLength(4)]
        public string? FechaCreacion { get; set; }
        public int? Calificacion { get; set; }
        public int GeneroId { get; set; }

        [ForeignKey("GeneroId")]
        public virtual Genero Genero { get; set; }
        public virtual ICollection<PeliculaPersonaje> Personajes { get; set; } 

    }
}
