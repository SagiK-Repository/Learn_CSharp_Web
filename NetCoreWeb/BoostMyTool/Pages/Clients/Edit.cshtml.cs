using BoostMyTool.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BoostMyTool.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new();
        public string ErrorMessage = string.Empty;
        public string SuccessMessage = string.Empty;

        public void OnGet()
        {
            string id = Request.Query["Id"]!;

            try
            {
                string connectionString = "Data Source=DESKTOP-IKKG899\\SQLEXPRESS;Initial Catalog=BoostMyTool;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM clients WHERE id=@id";
                    using SqlCommand command = new(sql, connection);
                    command.Parameters.AddWithValue("@id", id);
                    using SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        clientInfo.Id = reader.GetInt32(0);
                        clientInfo.Name = reader.GetString(1);
                        clientInfo.Email = reader.GetString(2);
                        clientInfo.Phone = reader.GetString(3);
                        clientInfo.Address = reader.GetString(4);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }
        }

        public void OnPost()
        {
            clientInfo.Id = Convert.ToInt32(Request.Form["Id"]!);
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
                string connectionString = "Data Source=DESKTOP-IKKG899\\SQLEXPRESS;Initial Catalog=BoostMyTool;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address " +
                                 "WHERE id=@id;";
                    using SqlCommand command = new(sql, connection);
                    command.Parameters.AddWithValue("@name", clientInfo.Name);
                    command.Parameters.AddWithValue("@email", clientInfo.Email);
                    command.Parameters.AddWithValue("@phone", clientInfo.Phone);
                    command.Parameters.AddWithValue("@address", clientInfo.Address);
                    command.Parameters.AddWithValue("@id", clientInfo.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
