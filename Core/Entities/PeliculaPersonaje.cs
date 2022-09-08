using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PeliculaPersonaje
    {
        public int PeliculaId { get; set; }
        public int PersonajeId { get; set; }

        [ForeignKey("PeliculaId")]
        public virtual Pelicula Pelicula { get; set; }

        [ForeignKey("PersonajeId")]
        public virtual Personaje Personaje { get; set; }



    }
}
