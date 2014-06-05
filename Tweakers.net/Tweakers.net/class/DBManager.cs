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
            Connect();
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

        public DataTable GetAlleCategorien()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT ID, Naam FROM Categorie";
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                dt.Load(cmd.ExecuteReader());
            }
            catch { return null; }
            finally { conn.Close(); }
            return dt;
        }

        public Categorie GetCategorie(int id)
        {
            int idcat = -1;
            string naam = null;
            int childof = -1;
            Categorie cat = null;
            try
            {
                if (id < 0)
                { throw new NoNullAllowedException(); }
                string sql = @"SELECT * FROM Categorie WHERE ID= :CatID";
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("CatID", OracleDbType.Int32, id, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idcat = reader.GetInt32(0);
                    naam = reader.GetOracleString(1).ToString();
                    string temp = reader.GetValue(3).ToString();
                    if (!int.TryParse(temp, out childof))
                    {
                        childof = -1;
                    }
                }
                cat = new Categorie(idcat, naam, null, childof);
            }
            catch { }
            finally
            {
                conn.Close();
            }
            return cat;
        }

        public DataTable GetCatProducts(int catID)
        {
            if (catID == 0)
            {
                DataTable dt = new DataTable();
                conn.Open();
                string sql = @"SELECT p.Naam, (SELECT min(prijs) FROM prijs WHERE product = p.naam) as Prijs FROM product p";
                try
                {
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("CatID", OracleDbType.Int32, catID, ParameterDirection.Input);
                    dt.Load(cmd.ExecuteReader());
                }
                catch { return null; }
                finally { conn.Close(); }
                return dt;
            }
            else
            {
                DataTable dt = new DataTable();
                conn.Open();
                string sql = @"SELECT p.Naam, (SELECT min(prijs) FROM prijs WHERE product = p.naam) as Prijs FROM product p WHERE Categorie = :CatID";
                try
                {
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("CatID", OracleDbType.Int32, catID, ParameterDirection.Input);
                    dt.Load(cmd.ExecuteReader());
                }
                catch { return null; }
                finally { conn.Close(); }
                return dt;
            }
        }

        public DataTable GetProductInfo(string naam)
        {
            string type = null;
            DataTable dt = new DataTable();
            conn.Open();
            string sql = @"SELECT soort FROM product WHERE naam = :Naam";
            try
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("Naam", OracleDbType.Varchar2, naam, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    type = reader.GetOracleString(0).ToString();
                }
            }
            catch { return null; }
            finally { conn.Close(); }
            switch (type)
            {
                    //Product is SSD
                case "ssd":
                    conn.Open();
                    sql = @"SELECT Naam, Merk, Geheugen_Capaciteit_gb as Capaciteit, Gewicht_gram as Gewicht, Afmetingen  FROM product a JOIN ssd b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    return dt;
                    //Product is Videokaart
                case "vid":
                    conn.Open();
                    sql = @"SELECT Naam, Merk, Kloksnelheid_mhz as Kloksneleid, Geheugen_capaciteit_gb as Geheugen, Geheugen_kloksnelheid_mhz as Mem_Kloksnelheid FROM product a JOIN videokaart b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    return dt;
                    //Product is Processor
                case "pro":
                    conn.Open();
                    sql = @"SELECT Naam, Merk, FROM product a JOIN processor b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    return dt;
                    //Product is Laptop
                case "lap":
                    conn.Open();
                    sql = @"SELECT Naam, Merk, FROM product a JOIN laptop b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    return dt;
                    //Product is Behuizing
                case "beh":
                    conn.Open();
                    sql = @"SELECT Naam, Merk, FROM product a JOIN behuizing b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    return dt;
                default:
                    return null;
            }
        }
    }
}