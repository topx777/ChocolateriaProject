using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChocolateriaForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = tbUsuario.Text;
            string codigo = tbCodigo.Text;

            string[] datos = new string[3];

            datos = Autentificacion.verificarAutentificacion(usuario, codigo);

            if (datos != null)
            {
                MessageBox.Show("Ingreso con exito");
            }
            else
            {
                MessageBox.Show("No se pudo Iniciar Sesion, el usuario no existe");
            }
        }
    }
}
