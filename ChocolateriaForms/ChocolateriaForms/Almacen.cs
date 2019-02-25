using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ChocolateriaForms
{
    class Almacen
    {
        public static string[] ObtenerAlmacen(int ID)
        {
            string[] datos = new string[2];

            string query = @"SELECT * FROM almacen WHERE id_almacen = @ID LIMIT 1";

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

        public static DataTable ListaAlmacenes()
        {
            string query = @"SELECT * FROM almacen";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, Conexion.obtenerConexion());

            DataTable almacenes = new DataTable();
            adapter.Fill(almacenes);

            return almacenes;
        }


        public static int CrearAlmacen(string Codigo)
        {
            string query1 = @"SELECT codigo_almacen FROM almacen 
                            WHERE codigo_almacen = @Codigo";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("Codigo", Codigo);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            string query = @"INSERT INTO almacen(codigo_almacen)) 
                            VALUES(@Codigo)";
            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());

            cmd.Parameters.AddWithValue("Codigo", Codigo);

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


        public static int ModificarAlmacen(int ID, string Codigo)
        {
            string query1 = @"SELECT codigo_almacen FROM almacen
                            WHERE codigo_almacen = @Codigo AND id_almacen != @ID";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("Nombre", Codigo);
            cmd1.Parameters.AddWithValue("ID", ID);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            string query = @"UPDATE almacen SET codigo_almacen = @Codigo
                            WHERE id_almacen = @ID";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("Codigo", Codigo);
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


        public static int EliminarAlmacen(int ID)
        {
            try
            {
                string query = @"DELETE FROM almacen WHERE id_almacen = @ID";

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
            catch(MySqlException)
            {
                return -2;       
            }
        }
    }
}
