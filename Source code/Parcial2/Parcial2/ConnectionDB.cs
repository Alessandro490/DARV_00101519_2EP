using System;
using System.Data;
using Npgsql;

namespace Parcial2
{
    public class ConnectionDB
    {
           private static string CadenaConexion =
            "Server=127.0.0.1;Port=5432; User Id=postgres; Password=uca; Database=Parcial2POO";
        
        //otras maneras de conectarse
          
          /*private static string 
              host = "127.0.0.1",
              dataBase = "Parcial2POO",
              userID = "postgres",
              Password = "uca";*/
            
        
        //  private static string CadenaConexion =
          //    $"Server={host};Port=5432;User Id={userID};Password={Password};Database={dataBase};"+
            //  "sslmode=Require;Trust Server Certificate=true ";

        public static DataTable realizarConsulta(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);
            DataSet ds = new DataSet();
            
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(ds);
            conn.Close();
            
            return ds.Tables[0];
        }

        public static void realizarAccion(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(CadenaConexion);
            
            conn.Open();
            NpgsqlCommand nc = new NpgsqlCommand(sql, conn);
            nc.ExecuteNonQuery();
            conn.Close();
        }
    }
}
      