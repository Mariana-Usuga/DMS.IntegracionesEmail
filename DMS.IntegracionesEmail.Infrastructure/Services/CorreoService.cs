using AE.Net.Mail;
using DMS.IntegracionesEmail.Core.Dtos;
using DMS.IntegracionesEmail.Core.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.IntegracionesEmail.Infrastructure.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly IConfiguration _configuration;
        private readonly ICursoRepository _cursoRepository;
        public CorreoService(IConfiguration configuration, ICursoRepository cursoRepository)
        {
            _configuration = configuration;
            _cursoRepository = cursoRepository;
        }
        public List<CorreoDto> ObtenerCorreos(string asunto)
        {
            var lista = new List<CorreoDto>();
            var servidorIMAP = _configuration["ConfiguracionIMAP:Servidor"];
            var puertoIMAP = int.Parse(_configuration["ConfiguracionIMAP:Puerto"]);
            var usaSSL = bool.Parse(_configuration["ConfiguracionIMAP:usaSSL"]);
            var email = _configuration["ConfiguracionIMAP:Email"];
            var claveEmail = _configuration["ConfiguracionIMAP:Clave"];
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                ImapClient client = new ImapClient(servidorIMAP, email, claveEmail, port: puertoIMAP, secure: usaSSL);
                client.SelectMailbox("INBOX");
                foreach (var uid in client.Search(SearchCondition.Subject(asunto)).Except(_cursoRepository.GetEmailSincronizados()))
                {
                    MailMessage mensaje = client.GetMessage(uid, false, false);
                    if(mensaje != null)
                    {
                        lista.Add(new CorreoDto { Uid = uid, Cuerpo = mensaje.Body });
                    }
                }
                client.Disconnect();
                return lista;

            }catch (Exception ex) {
                throw;
            }
        }

    }
}
