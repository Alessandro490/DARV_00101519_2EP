using Preparcial.Modelo;
using Preparcial.Controlador;
using System.Windows.Forms;
using System.Linq;
using System;

namespace Preparcial.Vista
{
    public partial class FrmMain : Form
    {
        private Usuario u;

        public FrmMain(Usuario u)
        {
            InitializeComponent();
            this.u = u;
        }

        private void bttnCreateUser_Click(object sender, EventArgs e)
        {
            if (!txtNewUser.Text.Equals(""))
            {
                ControladorUsuario.CrearUsuario(txtNewUser.Text);
                ActualizarCrearUsuario();
            }
            //Otra correcion seria que no se puede dejar un espacio en blanco y no nos muestra un mensaje de error
            //por lo que se añaderia un else por si al trarar de agregar un usario deja el espacio en blanco
            else
            {
                MessageBox.Show("Advertencia, no se puede dejar campos vacios");
            }
        }

        private void ActualizarCrearUsuario()
        {
            dgvCreateUser.DataSource = ControladorUsuario.GetUsuariosTable();
        }

        private void ActualizarInventario()
        {
            dgvInventary.DataSource = ControladorInventario.GetProductosTable();
        }

        private void ActualizarOrdenes()
        {
            dgvAllOrders.DataSource = ControladorPedido.GetPedidosTable();
        }

        private void ActualizarOrdenesUsuario()
        {
            dgvMyOrders.DataSource = ControladorPedido.GetPedidosUsuarioTable(u.IdUsuario);
            //Falta una orden a agregar, la cual es que el combo box se debe incializar en null
            //Esto se hace con objetivo de poblar el ComboBox con todos los productos a disposicion siendo propiedades 
            //de la clase usario
            //El orden para que funciones de manera correcta es, NULL, ValueMember, DisplayMember y luego DataSource
            
            cmbProductMakeOrder.DataSource = null;
            cmbProductMakeOrder.ValueMember = "idarticulo";
            cmbProductMakeOrder.DisplayMember = "producto";
            cmbProductMakeOrder.DataSource = ControladorInventario.GetProductos();
        }

        private void bttnAddInventary_Click(object sender, EventArgs e)
        {
            //En este if al poner && solo estaria veriricando que todos los campos esten parcialmente llenos y si hay uno vaío nos saltaría error
            //pero no tomaría acción mientras que al cambiarlo por un || nos saltaría el mensaje de que todos los campos deben de estar llenos
            //siendo mas óptimo ocupar || sobre && 
            if (txtProductNameInventary.Text.Equals("") ||
                txtDescriptionInventary.Text.Equals("") ||
                txtPriceInventary.Text.Equals("") ||
                txtStockInventary.Text.Equals(""))
                MessageBox.Show("No se puede dejar campos vacios");
            else
            {
                ControladorInventario.AnadirProducto(txtProductNameInventary.Text, txtDescriptionInventary.Text,
                    txtPriceInventary.Text, txtStockInventary.Text);

                ActualizarInventario();
            }
        }

        private void bttnDeleteInventary_Click(object sender, EventArgs e)
        {
            if(txtDeleteInventary.Text.Equals(""))
                MessageBox.Show("No puede dejar campos vacios");
            else
            {
                ControladorInventario.EliminarProducto(txtDeleteInventary.Text);
                ActualizarInventario();
            }
        }

        private void bttnUpdateStockInventary_Click(object sender, EventArgs e)
        {
            //aquí se debió cambiar && debido a que no agarra completamente los stock a actualizar sino más bien puede 
            //dejar algunos elementos vacíos por eso para evitar esa mala implementación se ocuparán ||
            //los cuales ocupamos previamente en una situación similar 
            if (txtUpdateStockIdInventary.Text.Equals("") ||  txtUpdateStockInventary.Text.Equals(""))
                MessageBox.Show("No puede dejar campos vacios");
            else
            {
                ControladorInventario.ActualizarProducto(txtUpdateStockIdInventary.Text, txtUpdateStockInventary.Text);
                ActualizarInventario();
            }
        }

        private void bttnMakeOrder_Click(object sender, EventArgs e)
        {
            if (txtMakeOrderQuantity.Text.Equals(""))
                MessageBox.Show("No puede dejar campos vacios");
            else
            {
                ControladorPedido.HacerPedido(u.IdUsuario, cmbProductMakeOrder.SelectedValue.ToString(), txtMakeOrderQuantity.Text);
                ActualizarOrdenesUsuario();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name.Equals("createNewUserTab") && u.Admin)
                ActualizarCrearUsuario();
            
            //Como se muestra en la sección de imágenes del repositorio, la tabla  inventario en la sección
            //de actualizar se encontraba mal nombrada pues decía eliminar en vez de actualizar a pesar que el
            //button nos decía actualizar
            
            else if (tabControl1.SelectedTab.Name.Equals("inventaryTab") && u.Admin)
                ActualizarInventario();

            else if (tabControl1.SelectedTab.Name.Equals("createOrderTab") && !u.Admin)
                ActualizarOrdenesUsuario();

            else if (tabControl1.SelectedTab.Name.Equals("viewOrdersTab") && u.Admin)
                ActualizarOrdenes();
            
            // Al dejar la setencia sin un quinto if no salta el mensaje de no tener permiso dos veces en vez de una 
            //por lo que se tendrá que agregar un if para evitar esa repetición erroneá de mensaje y solo debería 
            //únicamente en la zona a la cual no se tiene acceso
            
            //Añadir if para que no muestre mensaje de que no tiene permisos en la primera ventana
            else if(tabControl1.SelectedTab.Name.Equals("generalTab")) 
                tabControl1.SelectedTab = tabControl1.TabPages[0];
            
            else
            {
                MessageBox.Show("No tiene permisos para ver esta pestana");
                tabControl1.SelectedTab = tabControl1.TabPages[0];
            }

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
    Application.Exit();
        }
    }
}
