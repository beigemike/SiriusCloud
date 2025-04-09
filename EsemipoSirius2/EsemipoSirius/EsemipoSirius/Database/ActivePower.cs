using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EsemipoSirius.Database
{
    public class ActivePower
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";
        

        public List<string> DispositiviDisponibili()
        {
            List<string> elencoDispositivi = new List<string>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Device FROM DEVICE$";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string? dispositivo = reader["Device"].ToString();
                                elencoDispositivi.Add(dispositivo);
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
                return elencoDispositivi;
        }


        public List<ActivePowerDevice> getAll(string NomeDevice)
        {
            List<ActivePowerDevice> elenco = new List<ActivePowerDevice>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT DETTAGLIDEVICE$.Date, DETTAGLIDEVICE$.ActivePower " +
                        "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);
                     
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActivePowerDevice dispositivo = new ActivePowerDevice();

                                float ActPower;
                                bool testActPower = float.TryParse(reader["ActivePower"].ToString(), out ActPower);

                                if (testActPower)
                                {
                                    dispositivo.ActivePower = ActPower;
                                }
                                else
                                {
                                    dispositivo.ActivePower = null;
                                }


                                DateTime Date;
                                bool testDate = DateTime.TryParse(reader["Date"].ToString(), out Date);

                                if (testDate)
                                {
                                    dispositivo.Date = Date;
                                }
                                else
                                {
                                    dispositivo.Date = null;
                                }
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

