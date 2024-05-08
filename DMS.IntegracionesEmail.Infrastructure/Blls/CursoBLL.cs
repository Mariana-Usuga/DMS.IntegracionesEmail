using DMS.IntegracionesEmail.Core.Dtos;
using DMS.IntegracionesEmail.Core.Service;
using DMS.IntegracionesEmail.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace DMS.IntegracionesEmail.Infrastructure.Blls
{
    public class CursoBLL : ICursoBLL
    {
        private readonly IConfiguration _configuration;
        private readonly ICorreoService _correoService;
        private readonly ICursoRepository _cursoRepository;

        public CursoBLL(ICorreoService correoService, ICursoRepository cursoRepository, IConfiguration configuration) {
            _configuration = configuration;
            _correoService = correoService;
            _cursoRepository = cursoRepository;
        }
        public void Ejecutar()
        {
            var asunto = _configuration["AsuntoBusqueda"];
            try
            {
                var correos = _correoService.ObtenerCorreos(asunto);
                if (correos.Any()) { 
                    foreach(var correo in correos)
                    {
                        try
                        {
                            var lead = MapCorreo(correo.Cuerpo).ToObject<LeadDto>();
                            if (_cursoRepository.PutLead(lead))
                            {
                                _cursoRepository.PutEmailSincronizado(correo.Uid);
                            }
                            
                        }
                        catch (Exception ex)
                        {

                        }
                        //var lead = ProcesarCorreo(correo.Cuerpo);
                    }
                }
            }catch (Exception ex)
            {

            }
        }
        private LeadDto ProcesarCorreo(string correo)
        {
            var lead = new LeadDto();
            try
            {
                var lineas = correo.Split("\r\n");
                for (int i = 0; i < 8; i++)
                {
                    switch (i)
                    {
                        case 0: 
                            lead.Nombre = lineas[i].Split(":")[1].Trim(); 
                            break;
                        case 1:
                            lead.Celular = lineas[i].Split(":")[1].Trim();
                            break;
                        case 2:
                            lead.Fecha_Contacto = Convert.ToDateTime(lineas[i].Split(":")[1].Trim());
                            break;
                        case 3:
                            lead.Marca = lineas[i].Split(":")[1].Trim();
                            break;
                        case 4:
                            lead.Ciudad = lineas[i].Split(":")[1].Trim();
                            break;
                        case 5:
                            lead.Sede = lineas[i].Split(":")[1].Trim();
                            break;
                        case 6:
                            lead.Correo = lineas[i].Split(":")[1].Trim();
                            break;
                        case 7:
                            lead.Observaciones = lineas[i].Split(":")[1].Trim();
                            break;
                    }
                }
            }catch (Exception ex)
            {
                throw;
            }
            return lead;
        }
        private Dictionary<string, object> MapCorreo(string correo)
        {
            var lineas = correo.Split("\r\n").Where(linea => !string.IsNullOrWhiteSpace(linea)).ToArray(); ;
            var diccionario = new Dictionary<string, object>();
            try
            {
                foreach (var linea in lineas)
                {
                    var atributo = linea.Split(":");
                    if (atributo.Length > 0)
                    {
                        diccionario.Add(atributo[0].Trim(), atributo[1].Trim();
                    }
                }
                return diccionario;
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
