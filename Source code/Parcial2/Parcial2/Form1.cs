using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class Form1 : Form
    {
        private Usuario Usuario;

        public Form1(Usuario pUsuario)
        {
            InitializeComponent();
            Usuario = pUsuario;
            
           /*mostrar graficas unicas de un usario
            
            if (Usuario.admin)
            {
                graficoEstadisticas = new CartesianChart();
                this.Controls.Add(graficoEstadisticas);
                graficoEstadisticas.Parent = tabContenedor.TabPages[3];
            }*/
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblRegistrado.Text =
                "Bienvenido a la APP " + Usuario.usuario + " [" + (Usuario.admin ? "Administrador" : "Usuario") + "]";
           
            /* if (Usuario.admin) //acceso a diferente informacion
            {
                // Los administradores si tienen acceso a esta informacion
                configurarGrafico();
                actualizarControles();
            }
            else
            {
                // Los usuarios NO administradores no tienen permiso de acceder a estas pestanas
                tabContenedor.TabPages[1].Parent = null;
                tabContenedor.TabPages[1].Parent = null;
                tabContenedor.TabPages[1].Parent = null;
            }*/
        }
        
        /* //Actualiza los controles para los distintos usarios
        private void actualizarControles()
        {
            // Realizar consulta a la base de datos
            List<Usuario> lista = UsuarioInfo.getLista();
            
            // Tabla (data grid view)
            dgvEmpleados.DataSource = null;
            dgvEmpleados.DataSource = lista;
            // Menu desplegable (combo box)
            cmbUsuario.DataSource = null;
            cmbUsuario.ValueMember = "contrasena";
            cmbUsuario.DisplayMember = "usuario";
            cmbUsuario.DataSource = lista;
            // Grafico con estadisticas
            poblarGrafico();
        }*/
        
       /* Agregar datos a los graficos
       private void configurarGrafico()
       {
           graficoEstadisticas.Top = 10;
           graficoEstadisticas.Left = 10;
           graficoEstadisticas.Width = graficoEstadisticas.Parent.Width - 20;
           graficoEstadisticas.Height = graficoEstadisticas.Parent.Height - 20;

           graficoEstadisticas.Series = new SeriesCollection
           {
               new ColumnSeries{Title = "Cantidad de inicios de sesion", Values = new ChartValues<int>(), DataLabels = true}
           };
           graficoEstadisticas.AxisX.Add(new Axis{Labels = new List<string>()});
           graficoEstadisticas.AxisX[0].Separator = new Separator() {Step = 1, IsEnabled = false};
           graficoEstadisticas.LegendLocation = LegendLocation.Top;
       }
       
       private void poblarGrafico()
       {
           graficoEstadisticas.Series[0].Values.Clear();
           graficoEstadisticas.AxisX[0].Labels.Clear();
            
           foreach (Frecuencia f in UsuarioDAO.getEstadisticas())
           {
               graficoEstadisticas.Series[0].Values.Add(f.cantidad);
               graficoEstadisticas.AxisX[0].Labels.Add(f.usuario);
           }
        }*/

       /*
                   
        //Combo box actualizar pedidos
        cmbADDRESS.DataSource = null;
        cmbADDRESS.ValueMember = "idorder";
        cmbADDRESS.DisplayMember = "idorder";
        cmbPedido.DataSource = OrdenesDAO.getOrdenes();
            
        }*/

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir, " + Usuario.usuario + "?",
                "Parcial2", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    RegistroInfo.cerrarSesion(Usuario.usuario);

                    // No se pone el App.Exit() aquí porque volvería a llamar al evento
                    // form closing, ejecutando 2 veces el message box
                    e.Cancel = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ha sucedido un error, favor intente dentro de un minuto.",
                        "Parcial2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

      private void Form1_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            //Necesario porque el frmInicioSesion está escondido
            Application.Exit();
        }
      }
}