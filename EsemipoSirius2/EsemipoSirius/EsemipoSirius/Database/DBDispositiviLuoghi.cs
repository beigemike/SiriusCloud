using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;


namespace EsemipoSirius.Database
{
    public class DBDispositiviLuoghi
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";

        public List<DispositiviLuoghi> getNumDispositivi()
        {
            List<DispositiviLuoghi> dispositivi= new List<DispositiviLuoghi>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT PLANT$.Plant as Plant, COUNT(DEVICE$.Device) as Num FROM PLANT$ " +
                                    "inner join DEVICE$ on DEVICE$.IdPlant_FK = PLANT$.IdPlant " +
                                    "GROUP BY PLANT$.Plant " +
                                    "ORDER BY PLANT$.Plant";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DispositiviLuoghi dis = new DispositiviLuoghi();
                                dis.Plant = reader["Plant"].ToString();
                                dis.NumDevice = int.Parse(reader["Num"].ToString());
                                dispositivi.Add(dis);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DispositiviLuoghi dis = new DispositiviLuoghi();
                    dis.Plant = string.Empty;
                    dis.NumDevice = 0;
                }
            }
            return dispositivi;
        }
    }
}
