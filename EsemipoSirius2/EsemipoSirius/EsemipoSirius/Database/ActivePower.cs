using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EsemipoSirius.Database
{
    public class ActivePower
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";


        public List<ActivePowerDevice> getAll(string NomeDevice)
        {
            List<ActivePowerDevice> totActivePower = new List<ActivePowerDevice>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT CONVERT(DATE, DETTAGLIDEVICE$.Date) as Date, AVG(DETTAGLIDEVICE$.ActivePower) as ActivePower " +
                        "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device " +
                        "GROUP BY CONVERT(DATE, DETTAGLIDEVICE$.Date) " +
                        "ORDER BY CONVERT(DATE, DETTAGLIDEVICE$.Date)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ActivePowerDevice dispositivo = new ActivePowerDevice();
                                dispositivo.Date = DateTime.Parse(reader["Date"].ToString());
                                dispositivo.ActivePower = float.Parse(reader["ActivePower"].ToString());

                                totActivePower.Add(dispositivo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return totActivePower;
        }


   /*     public List<ActivePowerDevice?> Media(List<ActivePowerDevice?> activePower)
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
        } */
    }  
} 
