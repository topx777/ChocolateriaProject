using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ChocolateriaForms
{
    class Categoria
    {
        public static string[] ObtenerCategoria(int ID)
        {
            string[] datos = new string[2];

            string query = @"SELECT * FROM categoria WHERE id_categoria = @ID LIMIT 1";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("ID", ID);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    datos[0] = reader.GetInt32(0).ToString();
                    datos[1] = reader.GetString(1);
                }

                return datos;
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListaCategorias()
        {
            string query = @"SELECT * FROM categoria";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, Conexion.obtenerConexion());

            DataTable categorias = new DataTable();
            adapter.Fill(categorias);

            return categorias;
        }


        public static int CrearCategoria(string Nombre)
        {
            string query1 = @"SELECT nombre FROM categoria 
                            WHERE nombre = @Nombre";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("Nombre", Nombre);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            string query = @"INSERT INTO categoria(nombre) 
                            VALUES(@Nombre)";
            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());

            cmd.Parameters.AddWithValue("Nombre", Nombre);

            int realizado = cmd.ExecuteNonQuery();

            if (realizado > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }


        public static int ModificarCategoria(int ID, string Nombre)
        {
            string query1 = @"SELECT nombre FROM categoria
                            WHERE nombre = @Nombre AND id_categoria != @ID";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("Nombre", Nombre);
            cmd1.Parameters.AddWithValue("ID", ID);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            string query = @"UPDATE categoria SET nombre = @Nombre 
                            WHERE id_categoria = @ID";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("Nombre", Nombre);
            cmd.Parameters.AddWithValue("ID", ID);

            int realizado = cmd.ExecuteNonQuery();

            if (realizado > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }


        public static int EliminarCategoria(int ID)
        {
            string query = @"DELETE FROM categoria WHERE id_categoria = @ID";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("ID", ID);

            int realizado = cmd.ExecuteNonQuery();

            if (realizado > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}
