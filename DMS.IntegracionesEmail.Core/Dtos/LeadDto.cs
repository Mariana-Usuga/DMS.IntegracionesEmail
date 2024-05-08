using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.IntegracionesEmail.Core.Dtos
{
    public class LeadDto
    {
        public string Nombre { get; set; }
        public string Celular { get; set; }
        public DateTime Fecha_Contacto { get; set; }
        public string Marca { get; set; }
        public string Ciudad { get; set; }
        public string Sede { get; set; }
        public string Correo { get; set; }
        public string Observaciones { get; set; }



    }
}
