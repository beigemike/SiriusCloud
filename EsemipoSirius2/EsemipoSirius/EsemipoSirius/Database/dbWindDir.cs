using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;


namespace EsemipoSirius.Database
{
    public class dbWindDir
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";


        public List<WindDir> getWindDir(string NomeDevice)
        {
            List<WindDir> CoordDir = new List<WindDir>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    /*  string query = "SELECT AVG(DETTAGLIDEVICE$.[Nacelle Dir]) as Nacelle, AVG(DETTAGLIDEVICE$.[Wind Dir]) as Wind FROM DETTAGLIDEVICE$ " +
                           "INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                           "WHERE DEVICE$.Device = @Device " +
                           "GROUP BY CONVERT(DATE,DETTAGLIDEVICE$.Date)"; */

                    string query = "SELECT DETTAGLIDEVICE$.[Nacelle Dir] as Nacelle, DETTAGLIDEVICE$.[Wind Dir] as Wind FROM DETTAGLIDEVICE$ " +
                            "INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                            "WHERE DEVICE$.Device = @Device ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WindDir windDir = new WindDir();
                                string? Nacelle = reader["Nacelle"].ToString();
                                windDir.x = null;
                                if (float.TryParse(Nacelle, out float OutNacelle))
                                {
                                    windDir.x = OutNacelle;
                                }
                                string? Wind = reader["Wind"].ToString();
                                windDir.y = null;
                                if (float.TryParse(Wind, out float OutWind))
                                {
                                    windDir.y = OutWind;
                                }
                                CoordDir.Add(windDir);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return CoordDir;

        }

        public List<WindDir> getMediaDir(string NomeDevice)
        {
            List<WindDir> CoordDir = new List<WindDir>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT AVG(DETTAGLIDEVICE$.[Nacelle Dir]) as Nacelle, AVG(DETTAGLIDEVICE$.[Wind Dir]) as Wind FROM DETTAGLIDEVICE$ " +
                             "INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                             "WHERE DEVICE$.Device = @Device " +
                             "GROUP BY CONVERT(DATE,DETTAGLIDEVICE$.Date)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                WindDir windDir = new WindDir();
                                string? Nacelle = reader["Nacelle"].ToString();
                                windDir.x = null;
                                if (float.TryParse(Nacelle, out float OutNacelle))
                                {
                                    windDir.x = OutNacelle;
                                }
                                string? Wind = reader["Wind"].ToString();
                                windDir.y = null;
                                if (float.TryParse(Wind, out float OutWind))
                                {
                                    windDir.y = OutWind;
                                }
                                CoordDir.Add(windDir);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return CoordDir;

        }
    }
}
