using EsemipoSirius.Models;
using Microsoft.Data.SqlClient;

namespace EsemipoSirius.Database
{
    public class DBCosPhi
    {
        string Server = "(localdb)\\MSSQLLocalDB";
        string nomeDB = "DBSirius";

        public float? getMediaCosPhi(string NomeDevice)
        {
            List<CosPhi> elenco = new List<CosPhi>();
            string connectionString = "Server=" + Server + ";Database=" + nomeDB + ";Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT DETTAGLIDEVICE$.CosPhi " +
                        "FROM DETTAGLIDEVICE$ INNER JOIN DEVICE$ ON DEVICE$.IdDevice = DETTAGLIDEVICE$.IdDeviceFK " +
                        "WHERE DEVICE$.Device = @Device";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Device", NomeDevice);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CosPhi cosphi = new CosPhi();

                                float cosPhiValore;
                                bool testCosPhi = float.TryParse(reader["CosPhi"].ToString(), out cosPhiValore);

                                if (testCosPhi)
                                {
                                    cosphi.ValoreCosPhi = cosPhiValore;
                                }
                                else
                                {
                                    cosphi.ValoreCosPhi = null;
                                }
                                elenco.Add(cosphi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return media(elenco);
        }

        private float? media(List<CosPhi> elenco)
        {
            float? somma = 0;
            foreach (CosPhi a in elenco)
            {
                if (a.ValoreCosPhi != null)
                {
                    somma += a.ValoreCosPhi;
                }
            }
            return somma / elenco.Count;
        }

    }
}
