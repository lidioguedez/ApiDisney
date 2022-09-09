using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SendGridEnt
    {
        public string EmailDestinatario { get; set; }
        public string NombreDestinatario { get; set; }
        public string Contenido { get; set; }
        public string Nombre { get; set; }
        public string Token { get; set; }
    }
}
