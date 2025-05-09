using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EsemipoSirius.Database
{
    public class DBCosPhi
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";

        public CosPhi getMediaCosPhi(string NomeDevice)
        {
            CosPhi cosphi = new CosPhi();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT AVG(DETTAGLIDEVICE$.CosPhi) as CosPhi " +
                        "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                float valore = float.Parse(reader["CosPhi"].ToString());
                                cosphi.ValoreCosPhi = (float)Math.Round(valore, 2);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return cosphi;
        }


        public List<EfficienzaDispositivo> getEfficienzaCosPhi(string NomeDevice)
        {
            List<EfficienzaDispositivo> DettagliCosPhi = new List<EfficienzaDispositivo>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ROW_NUMBER() OVER (ORDER BY DETTAGLIDEVICE$.Date) AS NumeroRiga, DETTAGLIDEVICE$.Date as Date, " +
                        "DETTAGLIDEVICE$.CosPhi as CosPhi, DETTAGLIDEVICE$.[ActivePower] as ActivePower, DETTAGLIDEVICE$.[Reactive Power] as ReactivePower " +
                        "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EfficienzaDispositivo cosphi = new EfficienzaDispositivo();
                                cosphi.Id = int.Parse(reader["NumeroRiga"].ToString());
                                cosphi.Date = DateTime.Parse(reader["Date"].ToString());

                                if(reader["CosPhi"] != DBNull.Value)
                                {
                                    cosphi.CosPhi = float.Parse(reader["CosPhi"].ToString());
                                }
                                if (reader["ActivePower"] != DBNull.Value)
                                {
                                    cosphi.ActivePower = float.Parse(reader["ActivePower"].ToString());
                                }
                                if (reader["ReactivePower"] != DBNull.Value)
                                {
                                    cosphi.ReactivePower = float.Parse(reader["ReactivePower"].ToString());
                                }



                                DettagliCosPhi.Add(cosphi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return DettagliCosPhi;
        }

    }
}
