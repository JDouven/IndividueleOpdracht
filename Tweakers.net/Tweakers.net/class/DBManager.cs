//Database manager class for Tweakers
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Globalization;

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

        public bool CheckLogin(string username, string password)
        {
            string sql = @"SELECT wachtwoord FROM gebruiker WHERE gebruikersnaam = :Naam";
            conn.Open();
            try
            {
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("Naam", OracleDbType.Varchar2, username, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToString(reader.GetOracleString(0)).ToLower() == password.ToLower())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch { return false; }
            finally { conn.Close(); }
            return false;
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

        public Winkel GetWinkel(string naam)
        {
            Winkel w = null;
            string sql = @"SELECT naam, locatie, awards FROM Winkel WHERE Naam= :Naam";
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("Naam", OracleDbType.Varchar2, naam, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    w = new Winkel(reader.GetOracleString(0).ToString(),
                        reader.GetOracleString(1).ToString(),
                        reader.GetOracleString(2).ToString(),
                        new List<WinkelReview>());
                }
            }
            catch { }
            finally
            {
                conn.Close();
            }
            return w;
        }

        public Product GetProduct(string naam)
        {
            Product p = null;
            string pnaam = null;
            string merk = null;
            string afbeelding = null;
            string sql = @"SELECT naam, merk, afbeelding_url FROM Product WHERE Naam= :Naam";
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("Naam", OracleDbType.Varchar2, naam, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pnaam = reader.GetOracleString(0).ToString();
                    merk = reader.GetOracleString(1).ToString();
                    afbeelding = reader.GetOracleString(2).ToString();
                }
            }
            catch { }
            finally
            {
                conn.Close();
            }
            //Get Price list
            List<Prijs> prijzen = new List<Prijs>();
            sql = @"SELECT product, winkel, prijs FROM Prijs WHERE Product= :Naam";
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("Naam", OracleDbType.Varchar2, naam, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string Naam = Convert.ToString(reader.GetOracleString(0));
                    string winkel = Convert.ToString(reader.GetOracleString(1));
                    double prijs = Math.Round(Convert.ToDouble(reader.GetDouble(2)), 2);
                    prijzen.Add(new Prijs(Naam, winkel, prijs));
                }
            }
            catch { }
            finally
            {
                conn.Close();
            }
            //Get review IDs
            sql = @"SELECT review_id FROM Product_review WHERE Product_naam = :Naam";
            List<int> reviewID = new List<int>();
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("Naam", OracleDbType.Varchar2, naam, ParameterDirection.Input);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reviewID.Add(reader.GetInt32(0));
                }
            }
            catch { }
            finally
            {
                conn.Close();
            }
            //Get reviews list
            List<ProductReview> reviews = new List<ProductReview>();
            foreach (int i in reviewID)
            {
                try
                {
                    sql = @"SELECT Review_ID, auteur_gebruikersnaam as auteur, onderwerp, review_tekst as tekst, beoordeling FROM Review WHERE Review_ID = :ID";
                    conn.Open();
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("ID", OracleDbType.Int32, i, ParameterDirection.Input);
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        reviews.Add(new ProductReview(reader.GetInt32(0),
                            Convert.ToString(reader.GetOracleString(1)),
                            Convert.ToString(reader.GetOracleString(2)),
                            Convert.ToString(reader.GetOracleString(3)),
                            reader.GetInt32(4)));
                    }
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            p = new Product(pnaam, merk, afbeelding, prijzen, reviews);
            return p;
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
                    sql = @"SELECT a.Naam, a.Merk, b.Geheugen_Capaciteit_gb as Capaciteit, b.Gewicht_gram as Gewicht, b.Afmetingen  FROM product a JOIN ssd b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    break;
                //Product is Videokaart
                case "vid":
                    conn.Open();
                    sql = @"SELECT a.Naam, a.Merk, b.Kloksnelheid_mhz as Kloksneleid, b.Geheugen_capaciteit_gb as Geheugen, b.Geheugen_kloksnelheid_mhz as Geh_Kloksnelheid FROM product a JOIN videokaart b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    break;
                //Product is Processor
                case "pro":
                    conn.Open();
                    sql = @"SELECT a.Naam, a.Merk, b.Kloksnelheid_ghz as Kloksnelheid, b.Aantal_Cores, b.Hyperthreading FROM product a JOIN processor b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd1 = new OracleCommand(sql, conn);
                        cmd1.CommandType = CommandType.Text;
                        dt.Load(cmd1.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    break;
                //Product is Laptop
                case "lap":
                    conn.Open();
                    sql = @"SELECT a.Naam, a.Merk, b.Schermgrootte_inch as Schermgrootte, b.Gewicht_kg as gewicht, b.Processor, b.Videokaart FROM product a JOIN laptop b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    break;
                //Product is Behuizing
                case "beh":
                    conn.Open();
                    sql = @"SELECT a.Naam, a.Merk, b.Afmetingen, b.Gewicht_gram as Gewicht, b.Aantal_3_5_Inch_bays, b.Aantal_2_5_Inch_Bays FROM product a JOIN behuizing b ON a.naam = b.naam";
                    try
                    {
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch { return null; }
                    finally { conn.Close(); }
                    break;
                default:
                    return null;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                TextInfo tinfo = CultureInfo.InvariantCulture.TextInfo;
                dt.Columns[i].ColumnName = tinfo.ToTitleCase(tinfo.ToLower(dt.Columns[i].ColumnName));
            }
            return dt;
        }
    }
}