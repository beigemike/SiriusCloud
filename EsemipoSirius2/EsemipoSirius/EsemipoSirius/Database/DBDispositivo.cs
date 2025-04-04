using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EsemipoSirius.Database
{



    public class DBDispositivo
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";

        public List<Dispositivo> getDispositivi()
        {
            List<Dispositivo> elenco = new List<Dispositivo>();

            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM DEVICE$";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dispositivo dispositivo = new Dispositivo();

                                float altitude;
                                bool testaltitudine = float.TryParse(reader["altitude"].ToString(), out altitude);

                                if (testaltitudine)
                                {
                                    dispositivo.altitudine = float.Parse(reader["altitude"].ToString());
                                }
                                else
                                {
                                    dispositivo.altitudine = null;
                                }


                                float latitudine;
                                bool testlatitudine = float.TryParse(reader["latitude"].ToString(), out latitudine);

                                if (testaltitudine)
                                {
                                    dispositivo.latitudine = float.Parse(reader["latitude"].ToString());
                                }
                                else
                                {
                                    dispositivo.latitudine = null;
                                }

                                float longitude;
                                bool testlongitude = float.TryParse(reader["longitude"].ToString(), out longitude);

                                if (testlongitude)
                                {
                                    dispositivo.longitudine = float.Parse(reader["longitude"].ToString());
                                }
                                else
                                {
                                    dispositivo.longitudine = null;
                                }

                                dispositivo.IdDevice = int.Parse(reader["IdDevice"].ToString());
                                dispositivo.Device = reader["Device"].ToString();
                              /*  dispositivo.year = float.Parse(reader["year"].ToString());
                                dispositivo.up = reader["up"].ToString();
                                dispositivo.notes = reader["up"].ToString();
                                dispositivo.nominalPower = float.Parse(reader["nominalPower"].ToString());
                                dispositivo.techAvailability = reader["techAvailability"].ToString();
                                dispositivo.contrAvailability = reader["contrAvailability"].ToString();
                                dispositivo.municipality = reader["municipality"].ToString();
                                dispositivo.IdType_FK = int.Parse(reader["IdType_FK"].ToString());
                                dispositivo.IdLinea_FK = int.Parse(reader["IdLinea_FK"].ToString());
                                dispositivo.IdSubsystem_FK = int.Parse(reader["IdSubsystem_FK"].ToString());
                                dispositivo.IdModel_FK = int.Parse(reader["IdModel_FK"].ToString());
                                dispositivo.IdVendor_FK = int.Parse(reader["IdVendor_FK"].ToString());
                                dispositivo.IdSEE_FK = int.Parse(reader["Id_SEE_FK"].ToString());
                                dispositivo.IdEcName_FK = int.Parse(reader["IdEcName_FK"].ToString());
                                dispositivo.IdPlant_FK = int.Parse(reader["IdPlant_FK"].ToString());  */
                                elenco.Add(dispositivo);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return elenco;
        }

        public List<Dispositivo> GetDispositiviDaNominalPower(float valorePower)
        {
            List<Dispositivo> elenco = new List<Dispositivo>();

            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "select Device from DEVICE$ where nominalPower = @valoreNominalPower";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@valoreNominalPower", valorePower);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dispositivo dispositivo = new Dispositivo();
                                dispositivo.Device = reader["Device"].ToString();
                                elenco.Add(dispositivo);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return elenco;
        }


        public List<Dispositivo> getAtlLatLon(string device)
        {
            List<Dispositivo> elenco = new List<Dispositivo>();

            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "select altitude, latitude, longitude from DEVICE$ where Device = @NomeDevice";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NomeDevice", device);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dispositivo dispositivo = new Dispositivo();
                                dispositivo.altitudine = float.Parse(reader["altitude"].ToString());
                                dispositivo.latitudine = float.Parse(reader["latitude"].ToString());
                                dispositivo.longitudine = float.Parse(reader["longitude"].ToString());

                                elenco.Add(dispositivo);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return elenco;
        }

    }
}
