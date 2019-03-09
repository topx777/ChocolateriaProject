using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ChocolateriaForms
{
    class Ingrediente
    {

        //Metodo para ver solo una sucursal especifica
        public static string[] ObtenerIngrediente(int ID)
        {
            string[] datos = new string[2];

            string query = @"SELECT * FROM sucursal WHERE id_sucursal = @ID LIMIT 1";

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

        //Metodo para listar todas las sucursales
        public static DataTable ListaSucursales()
        {
            string query = @"SELECT * FROM sucursal";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, Conexion.obtenerConexion());

            DataTable sucursales = new DataTable();
            adapter.Fill(sucursales);

            return sucursales;
        }


        //Metodo para crear una nueva sucursal
        public static int CrearSucursal(string Nombre)
        {
            string query1 = @"SELECT nombre_sucursal FROM sucursal 
                            WHERE nombre_sucursal = @Nombre";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("Nombre", Nombre);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            string query = @"INSERT INTO sucursal(nombre_sucursal) 
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


        //Metodo para modificar una sucursal
        public static int ModificarSucursal(int ID, string Nombre)
        {
            string query1 = @"SELECT nombre_sucursal FROM sucursal
                            WHERE nombre_sucursal = @Nombre AND id_sucursal != @ID";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("Nombre", Nombre);
            cmd1.Parameters.AddWithValue("ID", ID);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            string query = @"UPDATE sucursal SET nombre_sucursal = @Nombre 
                            WHERE id_sucursal = @ID";

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


        //Metodo para eliminar una sucursal
        public static int EliminarSucursal(int ID)
        {
            string query = @"DELETE FROM sucursal WHERE id_sucursal = @ID";

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
