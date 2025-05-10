using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace EsemipoSirius.Database
{
    public class DBRotorGeneratorWind
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";

        public List<RotorGeneratorWind> getRotorWindGenSpeed(string NomeDevice)
        {
            List<RotorGeneratorWind> elenco = new List<RotorGeneratorWind>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT AVG(DETTAGLIDEVICE$.[Rotor Speed]) as RotorSpeed, AVG(DETTAGLIDEVICE$.[Wind Speed]) as WindSpeed, AVG(DETTAGLIDEVICE$.[Generator Speed]) as GeneratorSpeed " +
                            "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                            "WHERE DEVICE$.Device = @Device " +
                            "GROUP BY DATEADD(HOUR, DATEDIFF(HOUR, 0, DETTAGLIDEVICE$.Date), 0)";



                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RotorGeneratorWind velocità = new RotorGeneratorWind();

                                float RotorSpeed;
                                bool testRotor = float.TryParse(reader["RotorSpeed"].ToString(), out RotorSpeed);

                                if (testRotor)
                                {
                                    velocità.RotorSpeed = RotorSpeed;
                                }
                                else
                                {
                                    velocità.RotorSpeed = null;
                                }

                                float WindSpeed;
                                bool testWind = float.TryParse(reader["WindSpeed"].ToString(), out WindSpeed);

                                if (testWind)
                                {
                                    velocità.WindSpeed = WindSpeed;
                                }
                                else
                                {
                                    velocità.WindSpeed = null;
                                }

                                float GeneratorSpeed;
                                bool testGenerator = float.TryParse(reader["GeneratorSpeed"].ToString(), out GeneratorSpeed);

                                if (testGenerator)
                                {
                                    velocità.GeneratorSpeed = GeneratorSpeed;
                                }
                                else
                                {
                                    velocità.GeneratorSpeed = null;
                                }



                                elenco.Add(velocità);
                            }
                            return elenco;
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
