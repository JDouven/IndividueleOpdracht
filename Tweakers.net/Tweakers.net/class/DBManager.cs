//Database manager class for Tweakers
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

        private bool Connect()
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

        public Categorie GetCategorie(int id)
        {
            int idcat;
            string naam;
            int childof;
            Categorie cat = null;
            try
            {
                string sql = "SELECT * FROM Categorie WHERE ID= :CatID;";
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("CatID", OracleDbType.Int32, id, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idcat = reader.GetInt32(0);
                    naam = reader.GetOracleString(1).ToString();
                    childof = reader.GetInt32(3);
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