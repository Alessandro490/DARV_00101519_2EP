using System;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }
        
        private void frmInicioSesion_Load_1(object sender, EventArgs e)
        {
            poblarControles();
        }

        private void poblarControles()
        {
            // Actualizar ComboBox
            cmbUsuario.DataSource = null;
            cmbUsuario.ValueMember = "contrasena";
            cmbUsuario.DisplayMember = "usuario";
            cmbUsuario.DataSource = UsuarioInfo.getLista();
        }

        private void btnIniciarSesion_Click_1(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedValue.Equals(txtContrasena.Text))
            {
                Usuario up = (Usuario) cmbUsuario.SelectedItem;

                if (up.activo)
                {
                    RegistroInfo.iniciarSesion(up.usuario);
                    
                    MessageBox.Show("¡Bienvenido!", 
                        "Parcial2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                   Form1 ventana = new Form1(up);
                    ventana.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Su cuenta se encuentra inactiva, favor hable con el administrador", 
                        "Parcial2", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("¡Contraseña incorrecta!", "Parcial2",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtContrasena_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnIniciarSesion_Click_1(sender, e);
        }

        private void btnCambiarContra_Click_1(object sender, EventArgs e)
        {
            frmcambiarContraseña unaVentana = new frmcambiarContraseña();
            unaVentana.ShowDialog();
            poblarControles();
        }
    }
}