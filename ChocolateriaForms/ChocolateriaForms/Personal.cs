using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
    }
}
