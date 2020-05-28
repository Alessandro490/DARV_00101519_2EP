using System;
using System.Collections.Generic;
using System.Data;


namespace Parcial2
{
    public class UsuarioInfo
    {
        public static List<Usuario> getLista()
        {
            string sql = "select * from  APPUSER";
            //string sql  = "Select * FROM Table WHERE Title = @APPUSER";
            //sql.Parameters.Add(@title, Appuser)  ; 
            
            DataTable dt = ConnectionDB.realizarConsulta(sql);

            List<Usuario> lista = new List<Usuario>();
            foreach (DataRow fila in dt.Rows)
            {
                Usuario u = new Usuario();
                u.usuario = fila[0].ToString();
                u.contrasena = fila[1].ToString();
                u.admin = Convert.ToBoolean(fila[2].ToString());
                u.activo = Convert.ToBoolean(fila[3].ToString());

                lista.Add(u);
            }
            return lista;
        }

        public static void actualizarContra(string usuario, string nuevaContra)
        {
            string sql = String.Format(
                "update usuario set contrasena='{0}' where usuario='{1}';",
                nuevaContra, usuario);
            
            ConnectionDB.realizarAccion(sql);
        }
    }
}