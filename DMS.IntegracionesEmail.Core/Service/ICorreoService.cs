using DMS.IntegracionesEmail.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.IntegracionesEmail.Core.Service
{
    public interface ICorreoService
    {
        List<CorreoDto> ObtenerCorreos(string asunto);

    }
}
