using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Genero
    {
        public int GeneroId { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public virtual ICollection<Pelicula>? Pelicula { get; set; } 
    }
}
