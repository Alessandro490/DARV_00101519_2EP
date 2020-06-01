using System;
using System.Data;
using System.Windows.Forms;

namespace Preparcial.Controlador
{
    public static class ControladorPedido
    {
        public static DataTable GetPedidosUsuarioTable(string id)
        {
            DataTable pedidos = null;

            try
            {
                //Se debe cambiar la variable i.nombreArticulo pues eso no se encuentra en la base de datos
                //debe cambiar por i.nombreArt la cual se encuentra en la base de datos y nos permitirá 
                //agregar articulos a la base de datos
                pedidos = ConexionBD.EjecutarConsulta("SELECT p.idPedido, i.nombreArt, p.cantidad, i.precio, (i.precio * p.cantidad) AS total" +
                                                      " FROM PEDIDO p, INVENTARIO i, USUARIO u" +
                                                      " WHERE p.idArticulo = i.idArticulo" +
                                                      " AND p.idUsuario = u.idUsuario" +
                                                      $" AND u.idUsuario = {id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error");
            }

            return pedidos;
        }

        public static DataTable GetPedidosTable()
        {
            DataTable pedidos = null;

            try
            {
                //Se debe cambiar la variable i.nombreArticulo pues eso no se encuentra en la base de datos
                //Para realizar la consulta existe el mismo error que arriba pues no concuerda el nombre con la 
                //Base de datos con i.nombreArt ya se podrá almacenar
                pedidos = ConexionBD.EjecutarConsulta("SELECT p.idPedido, i.nombreArt, p.cantidad, i.precio, (i.precio * p.cantidad) AS total" +
                                                      " FROM PEDIDO p, INVENTARIO i, USUARIO u" +
                                                      " WHERE p.idArticulo = i.idArticulo" +
                                                      " AND p.idUsuario = u.idUsuario");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error");
            }

            return pedidos;
        }

        public static void HacerPedido(string idUsuario, string idArticulo, string cantidad)
        {
            try
            {
                ConexionBD.EjecutarComando("INSERT INTO PEDIDO(idUsuario, idArticulo, cantidad) " +
                    $"VALUES({idUsuario}, {idArticulo}, {cantidad})");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error");
            }
        }
    }
}
