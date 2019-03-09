using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace ChocolateriaForms
{
    class Pedido
    {
        public static int CrearPedido(int id_pedido,DateTime fecha_pedido,string telefono,string correo, int sucursal, decimal total_pedido, int usuario, int almacen)
        {
            //string query = @"SELECT id_pedido FROM pedido WHERE id_pedido=@id_pedido";

            //MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            //cmd.Parameters.AddWithValue("id_pedido", id_pedido);

            //MySqlDataReader reader = cmd.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    return -1;
            //}

            string query1 = @"INSERT INTO pedido(fecha_pedido,telefono,correo,sucursal,total_pedido,usuario,almacen)
                            VALUES (@fecha_pedido,@telefono,@correo,@sucursal,@total_pedido,@usuario,@almacen)";
            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("fecha_pedido", fecha_pedido);
            cmd1.Parameters.AddWithValue("telefono", telefono);
            cmd1.Parameters.AddWithValue("correo", correo);
            cmd1.Parameters.AddWithValue("sucursal", sucursal);
            cmd1.Parameters.AddWithValue("total_pedido", total_pedido);
            cmd1.Parameters.AddWithValue("usuario", usuario);
            cmd1.Parameters.AddWithValue("almacen", almacen);
            int realizado = cmd1.ExecuteNonQuery();
            if(realizado>0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static int EditarPedido(int id_pedido, DateTime fecha_pedido, string telefono, string correo, int sucursal, decimal total_pedido, int usuario, int almacen)
        {
            string query = @"SELECT id_pedido FROM pedido WHERE id_pedido=@id_pedido";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("id_pedido", id_pedido);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return -1;
            }
            string query1 = @"UPDATE pedido SET fecha_pedido=@fecha_pedido,telefono=@telefono,correo=@correo,sucursal=@sucursal,total_pedido=@total_pedido,usuario=@usuario,almacen=@almacen WHERE id_pedido=@id_pedido";
            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("id_pedido", id_pedido);
            cmd1.Parameters.AddWithValue("fecha_pedido", fecha_pedido);
            cmd1.Parameters.AddWithValue("telefono", telefono);
            cmd1.Parameters.AddWithValue("correo", correo);
            cmd1.Parameters.AddWithValue("sucursal", sucursal);
            cmd1.Parameters.AddWithValue("total_pedido", total_pedido);
            cmd1.Parameters.AddWithValue("usuario", usuario);
            cmd1.Parameters.AddWithValue("almacen", almacen);

        }
    }
}
