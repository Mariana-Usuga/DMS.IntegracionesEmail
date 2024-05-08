using DMS.IntegracionesEmail.Core.Dtos;
using DMS.IntegracionesEmail.Core.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.IntegracionesEmail.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly IConfiguration _configuration;
        public CursoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<string> GetEmailSincronizados()
        {
            var listaEmails = new List<string>();
            try
            {
                using SqlConnection conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var cmd = new SqlCommand("GetEmailSincronizados", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conexion.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listaEmails.Add(reader["uid"].ToString());
                }
                return listaEmails;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool PutEmailSincronizado(string uid)
        {
            try
            {
                using SqlConnection conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var cmd = new SqlCommand("PutEmailSincronizado", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("uid", uid);
                conexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool PutLead(LeadDto lead)
        {
            try
            {
                using SqlConnection conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var cmd = new SqlCommand("PutLead", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", lead.Nombre);
                cmd.Parameters.AddWithValue("@celular", lead.Celular);
                cmd.Parameters.AddWithValue("@fecha_contacto", lead.Fecha_Contacto);
                cmd.Parameters.AddWithValue("@marca", lead.Marca);
                cmd.Parameters.AddWithValue("@ciudad", lead.Ciudad);
                cmd.Parameters.AddWithValue("@sede", lead.Sede);
                cmd.Parameters.AddWithValue("@correo", lead.Correo);
                cmd.Parameters.AddWithValue("@observaciones", lead.Observaciones);
                conexion.Open();
                return cmd.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
