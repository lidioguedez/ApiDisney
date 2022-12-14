using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class PeliculaDetalleDto
    {
        public int PeliculaId { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }
        public string FechaCreacion { get; set; }
        public int Calificacion { get; set; }
        public int GeneroId { get; set; }
        public string GeneroNombre { get; set; }
        public ICollection<PeliculaPersonajeDto> data { get; set; }
    }
}
