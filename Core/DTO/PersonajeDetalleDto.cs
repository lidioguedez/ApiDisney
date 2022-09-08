using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class PersonajeDetalleDto
    {
        public int PersonajeId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
        public string Imagen { get; set; }
        public ICollection<PersonajePeliculaDto> data { get; set; }
    }
}
