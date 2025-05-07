using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;

namespace EsemipoSirius.Database
{
    public class ElencoDispositiviDisponibili
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
                    string query = "SELECT Device FROM DEVICE$ ORDER BY DEVICE$.Device";
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
    }
}
