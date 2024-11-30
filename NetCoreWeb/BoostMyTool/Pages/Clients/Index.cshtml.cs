using BoostMyTool.Application.Interfaces;
using BoostMyTool.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BoostMyTool.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly ISettings _settings;
        public List<ClientInfo> listClients = new();

        public IndexModel(ISettings settings)
        {
            _settings = settings;
        }

        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_settings.GetDBConnectionInfo()))
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
