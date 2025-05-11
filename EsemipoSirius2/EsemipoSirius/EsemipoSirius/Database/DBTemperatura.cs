using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;

namespace EsemipoSirius.Database
{
    public class DBTemperatura
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";


        public List<TemperaturaDisp> getAllTemperature(string NomeDevice)
        {
            List<TemperaturaDisp> totTemperature = new List<TemperaturaDisp>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT DETTAGLIDEVICE$.[Date] as Date,  DETTAGLIDEVICE$.[Controller HubTemp] as hubTemp, DETTAGLIDEVICE$.[Controller Top Temp] as TopTemp, " +
                        "DETTAGLIDEVICE$.[Ambient Temp] as AmbTmep, DETTAGLIDEVICE$.[Spinner Temp] as SpnTemp, " +
                        "DETTAGLIDEVICE$.[Hydraulic Oil Temp] as OilTemp, DETTAGLIDEVICE$.[Hydraulic Oil Pressure] as OilPress FROM DETTAGLIDEVICE$ " +
                        "INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TemperaturaDisp temp = new TemperaturaDisp();

                                string? date = reader["Date"].ToString();
                                temp.Date = null;
                                if (DateTime.TryParse(date, out DateTime OutDate))
                                {
                                    temp.Date = OutDate;
                                }

                                string? HubTemp = reader["hubTemp"].ToString(); 
                                temp.HubTemp = null;
                                if (float.TryParse(HubTemp, out float OutHub))
                                {
                                    temp.HubTemp = OutHub;
                                }

                                string? TopTemp = reader["TopTemp"].ToString();
                                temp.TopTemp = null;
                                if (float.TryParse(TopTemp, out float OutTop))
                                {
                                    temp.TopTemp = OutTop;
                                }

                                string? AmbTemp = reader["AmbTmep"].ToString();
                                temp.AmbTemp = null;
                                if (float.TryParse(AmbTemp, out float OutAmb))
                                {
                                    temp.AmbTemp = OutAmb;
                                }

                                string? SpinTemp = reader["AmbTmep"].ToString();
                                temp.SpinTemp = null;
                                if (float.TryParse(SpinTemp, out float OutSpin))
                                {
                                    temp.SpinTemp = OutSpin;
                                }

                                string? OilTemp = reader["OilTemp"].ToString();
                                temp.OilTemp = null;
                                if (float.TryParse(OilTemp, out float OutOil))
                                {
                                    temp.OilTemp = OutOil;
                                }

                                string? OilPress = reader["OilPress"].ToString();
                                temp.OilPress = null;
                                if (float.TryParse(OilPress, out float OutPress))
                                {
                                    temp.OilPress = OutPress;
                                }

                                totTemperature.Add(temp);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return totTemperature;

        }
    }
}
