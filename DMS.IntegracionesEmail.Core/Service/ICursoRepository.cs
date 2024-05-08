using DMS.IntegracionesEmail.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.IntegracionesEmail.Core.Service
{
    public interface ICursoRepository
    {
        bool PutLead(LeadDto lead);
        bool PutEmailSincronizado(string uid);
        List<string> GetEmailSincronizados();
    }
}
