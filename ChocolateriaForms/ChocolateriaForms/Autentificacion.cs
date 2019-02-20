using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ChocolateriaForms
{
    class Autentificacion
    {

        public static string[] verificarAutentificacion(string usuario, string codigo)
        {
            string[] datos = new string[3];

            string query = @"SELECT id_autentificacion, nombre_usuario, esAdmin FROM autentificacion 
                            WHERE nombre_usuario = @usuario AND codigo = @codigo";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("usuario", usuario);
            cmd.Parameters.AddWithValue("codigo", codigo);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    datos[0] = reader.GetInt32(0).ToString();
                    datos[1] = reader.GetString(1);
                    datos[2] = reader.GetBoolean(2).ToString();
                }

                return datos;
            }
            else
            {
                return null;
            }

        }
    }
}
