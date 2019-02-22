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
        public static int crearAutentificacion(string usuario, string codigo)
        {

            string query1 = @"SELECT nombre_usuario FROM autentificacion 
                            WHERE nombre_usuario = @usuario";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("usuario", usuario);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return -1;
            }

           string query = @"Insert into autentificacion(nombre_usuario ,codigo ,esAdmin) 
                            VALUES(@usuario, @codigo, 0)";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("usuario", usuario);
            cmd.Parameters.AddWithValue("codigo", codigo);

            int realizado= cmd.ExecuteNonQuery();

            if(realizado>0)
            {
                return int.Parse(cmd.LastInsertedId.ToString());
            }
            else
            {
                return -2;
            }
        }

        public static int modificarAutenticacion(int id, string usuario, string codigo)
        {

            string query1 = @"SELECT nombre_usuario FROM autentificacion 
                            WHERE nombre_usuario = @usuario AND id_autentificacion != @id";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("usuario", usuario);
            cmd1.Parameters.AddWithValue("id", id);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return -1;
            }

            string query = @"UPDATE autentificacion SET usuario = @usuario, codigo = @codigo WHERE id=@id";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("usuario", usuario);
            cmd.Parameters.AddWithValue("codigo", codigo);
            cmd.Parameters.AddWithValue("id", id);

            int realizado = cmd.ExecuteNonQuery();

            if (realizado > 0)
            {
                return 1;
            }
            else
            {
                return -2;
            }
        }
    }
}
