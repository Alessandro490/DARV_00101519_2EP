using System;
using System.Windows.Forms;

namespace Parcial2
{
    public partial class frmcambiarContraseña : Form
    {
        public frmcambiarContraseña()
        {
            InitializeComponent();
        }


        private void frmcambiarContraseña_Load(object sender, EventArgs e)
        {
            // Actualizar ComboBox
            cmbUsuario.DataSource = null;
            cmbUsuario.ValueMember = "contrasena";
            cmbUsuario.DisplayMember = "usuario";
            cmbUsuario.DataSource = UsuarioInfo.getLista();
        }


        private void btnCambiarContra_Click(object sender, EventArgs e)
        {
            bool actualIgual = cmbUsuario.SelectedValue.Equals(txtActual.Text);
            bool nuevaIgual = txtNueva.Text.Equals(txtRepetir.Text);
            bool nuevaValida = txtNueva.Text.Length > 0;

            if (actualIgual && nuevaIgual && nuevaValida)
            {
                try
                {
                    UsuarioInfo.actualizarContra(cmbUsuario.Text, txtNueva.Text);
                    
                    MessageBox.Show("¡Contraseña actualizada exitosamente!", 
                        "Parcial2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("¡Contraseña no actualizada! Favor intente mas tarde.", 
                        "Clase GUI 05", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("¡¡Favor verifique que los datos sean correctos!", 
                    "Clase GUI 05", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}