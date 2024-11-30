using BoostMyTool.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BoostMyTool.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-IKKG899\\SQLEXPRESS;Initial Catalog=BoostMyTool;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from clients";
                    using SqlCommand command = new(sql, connection);
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ClientInfo clientInfo = new ClientInfo()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Address = reader.GetString(4),
                            Created = reader.GetDateTime(5).ToString(),
                        };

                        listClients.Add(clientInfo);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
