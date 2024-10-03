using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Conexion
    {
        private string cadenaSQL = string.Empty;

        public Conexion()
        {
            // Inicializar la cadena de conexión directamente
            cadenaSQL = "Data Source=(local); Initial Catalog=pruebaTecnica;Integrated Security=true";
        }

        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}
