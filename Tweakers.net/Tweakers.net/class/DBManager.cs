using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace Tweakers
{
    public class DBManager
    {
        private OracleConnection conn;

        public DBManager()
        {
            conn = new OracleConnection();
        }

        private bool connect()
        {
            try
            {
                conn.ConnectionString = "User Id= tweakers; Password= password; Data Source= localhost:1521;";
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Categorie GetCategorie(int ID)
        {
            Categorie cat = null;
            try
            {
                string sql = "SELECT * FROM Categorie WHERE ID= :CatID;";
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("CatID", OracleDbType.Int32, ID, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);

                }

            }
            catch { }
            finally
            {
                conn.Close();
            }

            return cat;
        }
    }
}