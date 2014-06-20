using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace Container_Shipping_Company
{
    public class DatabaseManager
    {
        private OracleConnection conn;

        /// <summary>
        /// Database manager voor Container Shipping Company
        /// </summary>
        public DatabaseManager()
        {
            conn = new OracleConnection();
        }

        /// <summary>
        /// Opent de verbinding met de DB
        /// </summary>
        private void Open()
        {
            try
            {
                conn.ConnectionString = "User Id= lp; Password= password; Data Source= localhost:1521;";
                conn.Open();
            }
            catch
            { }
        }

        //Geeft een lijst met alle Containers
        public List<Container> GetContainers()
        {
            List<Container> containers = new List<Container>();
            Open();
            try
            {
                string sql = @"SELECT c.ID, c.NaamBedrijf, c.Bestemming, b.land, c.type, c.gewicht, c.Ingeplanned FROM container c LEFT JOIN bestemming b ON c.bestemming = b.naam";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //Zet character om naar enum
                    string stringtype = (string)reader["type"];
                    ContainerType type = ContainerType.N;
                    if (stringtype == "N")
                        type = ContainerType.N;
                    else if (stringtype == "V")
                        type = ContainerType.V;
                    else if (stringtype == "C")
                        type = ContainerType.C;

                    //Zet character om naar boolean
                    string stringplanned = (string)reader["ingeplanned"];
                    bool ingeplanned;
                    if (stringplanned == "n")
                        ingeplanned = false;
                    else
                        ingeplanned = true;

                    containers.Add(new Container(Convert.ToInt32(reader["ID"]),
                        (string)reader["NaamBedrijf"],
                        new Bestemming((string)reader["Bestemming"],
                            (string)reader["Land"]),
                        Convert.ToInt32(reader["gewicht"]),
                        type,
                        ingeplanned));
                }
            }
            catch
            {
                containers = null;
            }
            finally
            {
                conn.Close();
            }
            return containers;
        }

        /// <summary>
        /// Geeft een lijst met alle Containertruckingbedrijven
        /// </summary>
        /// <returns></returns>
        public List<Containertruckingbedrijf> GetContainertruckingbedrijven()
        {
            List<Containertruckingbedrijf> bedrijven = new List<Containertruckingbedrijf>();
            Open();
            try
            {
                string sql = @"SELECT * FROM Containertruckingbedrijf";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bedrijven.Add(new Containertruckingbedrijf((string)reader["naam"],
                        (string)reader["contactpersoonnaam"],
                        Convert.ToInt32(reader["kvknummer"])));
                }
            }
            catch
            {
                bedrijven = null;
            }
            finally
            {
                conn.Close();
            }
            return bedrijven;
        }

        /// <summary>
        /// Geeft een lijst met alle bestemmingen
        /// </summary>
        /// <returns></returns>
        public List<Bestemming> GetBestemmingen()
        {
            List<Bestemming> bestemmingen = new List<Bestemming>();
            Open();
            try
            {
                string sql = @"SELECT * FROM Bestemming";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    bestemmingen.Add(new Bestemming((string)reader["naam"],
                        (string)reader["land"]));
                }
            }
            catch
            {
                bestemmingen = null;
            }
            finally
            {
                conn.Close();
            }
            return bestemmingen;
        }

        /// <summary>
        /// Geeft een lijst van alle schepen
        /// </summary>
        /// <returns></returns>
        public List<Schip> GetSchepen()
        {
            List<Schip> schepen = new List<Schip>();
            Open();
            try
            {
                string sql = @"SELECT * FROM Schip";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    schepen.Add(new Schip((string)reader["type"],
                        Convert.ToInt32(reader["hoogte"]),
                        Convert.ToInt32(reader["rijen"]),
                        Convert.ToInt32(reader["containersperrij"]),
                        Convert.ToInt32(reader["stroomaansluitingen"])));
                }
            }
            catch
            {
                schepen = null;
            }
            finally
            {
                conn.Close();
            }
            return schepen;
        }

        /// <summary>
        /// Zet de waarde 'ingepland' van een container op true('y')
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SetContainerIngepland(int id)
        {
            bool success = false;
            Open();
            try
            {
                string sql = @"UPDATE container SET ingeplanned='y' WHERE ID = :id";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("id", OracleDbType.Int32, id, ParameterDirection.Input);
                cmd.CommandType = CommandType.Text;
                if (cmd.ExecuteNonQuery() == 0)
                    success = false;
                else
                    success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Geeft het hoogste ID van de containers
        /// </summary>
        /// <returns></returns>
        private int GetMaxContainerID()
        {
            int result = -1;
            try
            {
                Open();
                string sql = @"SELECT max(ID) FROM container";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                result = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            }
            catch
            {
                result = -1;
            }
            finally { conn.Close(); }
            return result;
        }

        /// <summary>
        /// Voegt een Container toe aan de DB
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public bool AddContainer(Container container)
        {
            bool success = false;
            
            try
            {
                string sql = @"INSERT INTO Container VALUES( :id, :naambedrijf, :bestemming, :type, :gewicht, 'n', null)";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("id", OracleDbType.Int32, GetMaxContainerID(), ParameterDirection.Input);
                cmd.Parameters.Add("naambedrijf", OracleDbType.Varchar2, container.Containertruckingbedrijf, ParameterDirection.Input);
                cmd.Parameters.Add("bestemming", OracleDbType.Varchar2, container.CBestemming.Naam, ParameterDirection.Input);
                cmd.Parameters.Add("type", OracleDbType.Varchar2, container.Type.ToString(), ParameterDirection.Input);
                cmd.Parameters.Add("gewicht", OracleDbType.Int32, container.Gewicht, ParameterDirection.Input);
                cmd.CommandType = CommandType.Text;
                Open();
                if (cmd.ExecuteNonQuery() == 0)
                    success = false;
                else
                    success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Voegt een Containertruckingbedrijf toe aan de DB
        /// </summary>
        /// <param name="bedrijf"></param>
        /// <returns></returns>
        public bool AddContainertruckingbedrijf(Containertruckingbedrijf bedrijf)
        {
            bool success = false;

            try
            {
                string sql = @"INSERT INTO Containertruckingbedrijf VALUES( :naam, :naamcontact, :kvknummer, null)";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("naam", OracleDbType.Varchar2, bedrijf.Naam, ParameterDirection.Input);
                cmd.Parameters.Add("naamcontact", OracleDbType.Varchar2, bedrijf.ContactpersoonNaam, ParameterDirection.Input);
                cmd.Parameters.Add("kvknummer", OracleDbType.Int32, bedrijf.KvKNummer, ParameterDirection.Input);
                cmd.CommandType = CommandType.Text;
                Open();
                if (cmd.ExecuteNonQuery() == 0)
                    success = false;
                else
                    success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Voegt een Schip toe aan de DB
        /// </summary>
        /// <param name="schip"></param>
        /// <returns></returns>
        public bool AddSchip(Schip schip)
        {
            bool success = false;

            try
            {
                string sql = @"INSERT INTO Schip VALUES( :naam, :hoogte, :rijen, :containersperrij, :stroomaansluitingen)";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("naam", OracleDbType.Varchar2, schip.Type, ParameterDirection.Input);
                cmd.Parameters.Add("hoogte", OracleDbType.Int32, schip.Hoogte, ParameterDirection.Input);
                cmd.Parameters.Add("rijen", OracleDbType.Int32, schip.Rijen, ParameterDirection.Input);
                cmd.Parameters.Add("containersperrij", OracleDbType.Int32, schip.ContainersPerRij, ParameterDirection.Input);
                cmd.Parameters.Add("stroomaansluitingen", OracleDbType.Int32, schip.StroomAansluitingen, ParameterDirection.Input);
                cmd.CommandType = CommandType.Text;
                Open();
                if (cmd.ExecuteNonQuery() == 0)
                    success = false;
                else
                    success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        /// <summary>
        /// Voegt een Bestemming toe aan de DB
        /// </summary>
        /// <param name="bestemming"></param>
        /// <returns></returns>
        public bool AddBestemming(Bestemming bestemming)
        {
            bool success = false;

            try
            {
                string sql = @"INSERT INTO Bestemming VALUES( :naam, :land)";
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add("naam", OracleDbType.Varchar2, bestemming.Naam, ParameterDirection.Input);
                cmd.Parameters.Add("land", OracleDbType.Varchar2, bestemming.Land, ParameterDirection.Input);
                cmd.CommandType = CommandType.Text;
                Open();
                if (cmd.ExecuteNonQuery() == 0)
                    success = false;
                else
                    success = true;
            }
            catch
            {
                success = false;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
    }
}
