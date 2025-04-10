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
                            List<ActivePowerDevice> totActivePower = new List<ActivePowerDevice>();
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
                                totActivePower.Add(dispositivo);
                                elenco.Add(dispositivo);
                            }
                           return Media(totActivePower);
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


       public List<ActivePowerDevice?> Media(List<ActivePowerDevice?> activePower)
       {
            List<DateTime?> date = new List<DateTime?>();
            foreach (ActivePowerDevice a in activePower)
            {
                date.Add(a.Date);
            }
            List<DateTime?> dateOrdinate = date.OrderBy(n => n).ToList();



            DateTime? inizio = dateOrdinate[0];
            DateTime? fine = dateOrdinate[dateOrdinate.Count - 1];
            DateTime? dataSuccessiva = inizio?.AddDays(1);
            List<ActivePowerDevice?> Medie = new List<ActivePowerDevice?>();
            
            List<ActivePowerDevice> sommaActivePower = new List<ActivePowerDevice>();
            float? somma = 0;
            float count = 0;
            while (dataSuccessiva < fine)
            {
                ActivePowerDevice mediaActPwr = new ActivePowerDevice();
                foreach (ActivePowerDevice a in activePower)
                {
                    if (a.Date < dataSuccessiva && a.ActivePower != null)
                    {
                        somma += a.ActivePower;
                        count++;
                    }
                }
                float? media = somma / count;
                mediaActPwr.ActivePower = media;
                mediaActPwr.Date = dataSuccessiva;
                Medie.Add(mediaActPwr);
                dataSuccessiva = dataSuccessiva?.AddDays(1);
            }
            return Medie;
        }
    }
}
