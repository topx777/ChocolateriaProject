using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ChocolateriaForms
{
    class Conexion
    {

        public static MySqlConnection obtenerConexion()
        {
            MySqlConnection connection = new MySqlConnection("server=127.0.0.1;database = chocobd;Uid = root;pwd=;SslMode = none;");

            connection.Open();

            return connection;
        }

    }
}
