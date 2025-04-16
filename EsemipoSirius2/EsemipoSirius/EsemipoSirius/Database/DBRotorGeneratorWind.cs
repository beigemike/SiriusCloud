using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;

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
                    string query = "SELECT DETTAGLIDEVICE$.[Rotor Speed], DETTAGLIDEVICE$.[Wind Speed], DETTAGLIDEVICE$.[Generator Speed] " +
                        "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RotorGeneratorWind velocità = new RotorGeneratorWind();

                                float RotorSpeed;
                                bool testRotor = float.TryParse(reader["Rotor Speed"].ToString(), out RotorSpeed);

                                if (testRotor)
                                {
                                    velocità.RotorSpeed = RotorSpeed;
                                }
                                else
                                {
                                    velocità.RotorSpeed = null;
                                }

                                float WindSpeed;
                                bool testWind = float.TryParse(reader["Wind Speed"].ToString(), out WindSpeed);

                                if (testWind)
                                {
                                    velocità.WindSpeed = WindSpeed;
                                }
                                else
                                {
                                    velocità.WindSpeed = null;
                                }

                                float GeneratorSpeed;
                                bool testGenerator = float.TryParse(reader["Generator Speed"].ToString(), out GeneratorSpeed);

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
