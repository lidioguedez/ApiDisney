using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class PeliculaDataDto
    {
        public string Titulo { get; set; }
        public string? Imagen { get; set; }
        public string? FechaCreacion { get; set; }
        public int? Calificacion { get; set; }
        public int? GeneroId { get; set; }
        public List<int>? PersonajeID { get; set; }

    }
}
