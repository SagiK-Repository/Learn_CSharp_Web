using BoostMyTool.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BoostMyTool.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly ConnectionDBInfo _connectionDBInfo;
        public ClientInfo clientInfo = new();
        public string ErrorMessage = string.Empty;
        public string SuccessMessage = string.Empty;

        public CreateModel(ConnectionDBInfo connectionDBInfo)
        {
            _connectionDBInfo = connectionDBInfo; 
        }

        public void OnPost()
        {
            clientInfo.Name = Request.Form["Name"]!;
            clientInfo.Email = Request.Form["Email"]!;
            clientInfo.Phone = Request.Form["Phone"]!;
            clientInfo.Address = Request.Form["Address"]!;

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 || clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0)
            {
                ErrorMessage = "All the fields are required";
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionDBInfo.DBConnection))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";
                    using SqlCommand command = new(sql, connection);
                    command.Parameters.AddWithValue("@name", clientInfo.Name);
                    command.Parameters.AddWithValue("@email", clientInfo.Email);
                    command.Parameters.AddWithValue("@phone", clientInfo.Phone);
                    command.Parameters.AddWithValue("@address", clientInfo.Address);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }

            clientInfo.Name = "";
            clientInfo.Email = "";
            clientInfo.Phone = "";
            clientInfo.Address = "";
            SuccessMessage = "New Client Added Correctly";

            Response.Redirect("/Clients/Index");
        }
    }
}
