using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class PeliculaDto
    {
        public int PeliculaId { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string FechaCreacion { get; set; }
        public string GeneroNombre { get; set; }
    }
}
