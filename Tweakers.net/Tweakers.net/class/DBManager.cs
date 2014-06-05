using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;


namespace Tweakers.net
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
                conn.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public DataTable GetProducts()
        {
            DataTable dt = new DataTable();


            return dt;
        }
	}
}