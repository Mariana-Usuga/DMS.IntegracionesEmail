using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.IntegracionesEmail.Infrastructure.Extensions
{
    public static class DiccionaryExtension
    {
        public static T ToObject<T>(this Dictionary<string, object> fuente) where T : class, new()
        {
            T obj = new T();
            var propiedades = obj.GetType().GetProperties();
            foreach (var propiedad in propiedades)
            {
                if(fuente.TryGetValue(propiedad.Name.ToLower(), out var val))
                {
                    if (propiedad.Name.ToLower().Equals("fecha_contacto"))
                    {
                        propiedad.SetValue(obj, Convert.ToDateTime(val));
                    }
                    else
                    {
                        propiedad.SetValue(obj, val);
                    }
                    
                }
            }
            return obj;
        }
    }
}
