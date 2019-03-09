using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace ChocolateriaForms
{
    public class Personal
    {
        //Entra Metodos de Personal y al final Metodos de Autentificacion
        public static int CrearPersonal(string CI,string Nombre,string SegundoNombre,string PrimerApellido, string SegundoApellido,DateTime FechaNacimiento,int Turno, DateTime HoraInicio, DateTime HoraFin ,string Usuario, string Codigo)
        {
            string query1 = @"SELECT ci FROM personal 
                            WHERE ci = @CI";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("CI", CI);

            MySqlDataReader reader = cmd1.ExecuteReader();

            if (reader.HasRows)
            {
                return 0;
            }

            int ResultadoAutentificacion= Autentificacion.crearAutentificacion(Usuario, Codigo);

            var nuevaFechaNacimiento = FechaNacimiento.Date;
            var nuevaHoraInicio = HoraInicio.TimeOfDay;
            var nuevaHoraFin = HoraFin.TimeOfDay;


            if (ResultadoAutentificacion > 0)
            {
                string query = @"Insert into Personal(ci,nombre,segundo_nombre,primer_apellido,segundo_apellido,autentificacion,fecha_nacimiento,turno,hora_inicio,hora_fin)
                                VALUES(@CI,@Nombre,@SegundoNombre,@PrimerApellido,@SegundoApellido,@ResultadoAutentificacion ,@nuevaFechaNacimiento,@Turno,@nuevaHoraInicio,@nuevaHoraFin)";

                MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
                cmd.Parameters.AddWithValue("CI", CI);
                cmd.Parameters.AddWithValue("Nombre", Nombre);
                cmd.Parameters.AddWithValue("SegundoNombre", SegundoNombre);
                cmd.Parameters.AddWithValue("PrimerApellido", PrimerApellido);
                cmd.Parameters.AddWithValue("SegundoApellido", SegundoApellido);
                cmd.Parameters.AddWithValue("ResultadoAutentificacion", ResultadoAutentificacion);
                cmd.Parameters.AddWithValue("FechaNacimiento", nuevaFechaNacimiento);
                cmd.Parameters.AddWithValue("Turno", Turno);
                cmd.Parameters.AddWithValue("nuevaHoraInicio", nuevaHoraInicio);
                cmd.Parameters.AddWithValue("nuevaHoraFin", nuevaHoraFin);

                int realizado = cmd.ExecuteNonQuery();

                if (realizado > 0)
                {
                    return 1;
                }


                return -1;
            }
            else
            {
                return -2;
            }
        }

        public static int ModificarPersonal(int id, string CI, string Nombre, string SegundoNombre, string PrimerApellido, string SegundoApellido, DateTime FechaNacimiento, int Turno, DateTime HoraInicio, DateTime HoraFin, string Usuario, string Codigo)
        {
            string query1 = @"SELECT ci, autentificacion FROM personal   
                            WHERE id = @ID";

            MySqlCommand cmd1 = new MySqlCommand(query1, Conexion.obtenerConexion());
            cmd1.Parameters.AddWithValue("ID", id);

            MySqlDataReader reader = cmd1.ExecuteReader();

            int id_autentificacion = 0;
            if (reader.HasRows)
            {
                id_autentificacion = reader.GetInt32(1);
            }

            int ResultadoAutentificacion = Autentificacion.modificarAutenticacion(id_autentificacion, Usuario,Codigo);

            var nuevaFechaNacimiento = FechaNacimiento.Date;
            var nuevaHoraInicio = HoraInicio.TimeOfDay;
            var nuevaHoraFin = HoraFin.TimeOfDay;

            if (ResultadoAutentificacion>0)
            {
                string query = @"UPDATE personal SET nombre=@Nombre,segundo_nombre=@SegundoNombre,primer_apellido=@PrimerApellido, 
                               segundo_apellido=@SegundoApellido,fecha_nacimiento=@FechaNacimiento,turno=@Turno,hora_inicio=@HoraInicio,hora_fin=@HoraFin WHERE id=@ID";

                MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
                cmd.Parameters.AddWithValue("ID", id);
                cmd.Parameters.AddWithValue("Nombre", Nombre);
                cmd.Parameters.AddWithValue("SegundoNombre", SegundoNombre);
                cmd.Parameters.AddWithValue("PrimerApellido", PrimerApellido);
                cmd.Parameters.AddWithValue("SegundoApellido", SegundoApellido);
                cmd.Parameters.AddWithValue("FechaNacimiento", nuevaFechaNacimiento);
                cmd.Parameters.AddWithValue("Turno", Turno);
                cmd.Parameters.AddWithValue("HoraInicio", nuevaHoraInicio);
                cmd.Parameters.AddWithValue("HoraFin", nuevaHoraFin);

                int realizado = cmd.ExecuteNonQuery();

                if(realizado>0)
                {
                    return 1;
                }

                else
                {
                    return -1;
                }

            }

            else
            {
                return -2;
            }
        }

        public static DataTable verListaPersonal()
        {
            DataTable Data = new DataTable();

            string query = @"SELECT personal.id,personal.ci,personal.nombre,personal.segundo_nombre,personal.primer_apellido,personal.segundo_apellido,personal.fecha_nacimiento,turno.nombre AS nombre_turno,personal.hora_inicio,personal.hora_fin FROM personal INNER JOIN turno ON personal.turno=turno.id_turno INNER JOIN autentificacion ON personal.autentificacion=autentificacion.id_autentificacion WHERE autentificacion.esAdmin = 0";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.Fill(Data);

            return Data;
        }

        public static string[] verDetallePersonal(int id)
        {
            string[] datos = new string[15];

            string query = @"SELECT personal.id,personal.ci,personal.nombre,personal.segundo_nombre,personal.primer_apellido,personal.segundo_apellido,personal.fecha_nacimiento,personal.fecha_registro,turno.nombre AS nombre_turno,personal.hora_inicio,personal.hora_fin,autentificacion.nombre_usuario,autentificacion,codigo,autentificacion.esAdmin FROM personal INNER JOIN turno ON personal.turno=turno.id_turno INNER JOIN autentificacion ON personal.autentificacion=autentificacion.id_autentificacion WHERE personal.id=@id";

            MySqlCommand cmd = new MySqlCommand(query, Conexion.obtenerConexion());
            cmd.Parameters.AddWithValue("id", id);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    datos[0] = reader.GetInt32(0).ToString();   //id
                    datos[1] = reader.GetString(1);     //ci
                    datos[2] = reader.GetString(2);     //nombre
                    datos[3] = reader.GetString(3);     //segundo_nombre
                    datos[4] = reader.GetString(4);     //primer_apellido
                    datos[6] = reader.GetString(5);     //segundo_apellido
                    datos[7] = reader.GetDateTime(6).ToString();    //fecha_nacimiento
                    datos[8] = reader.GetDateTime(7).ToString();    //fecha_registro
                    datos[9] = reader.GetString(8);     //nombre_turno
                    datos[10] = reader.GetDateTime(9).ToString();        //hora_inicio
                    datos[11] = reader.GetDateTime(10).ToString();        //hora_fin
                    datos[12] = reader.GetString(11);        //nombre_usuario
                    datos[13] = reader.GetString(12);        //Codigo
                    datos[14] = reader.GetBoolean(13).ToString();        //esAdmin

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
