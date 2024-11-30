using BoostMyTool.Application.Interfaces;
using MediatR;
using System.Data.SqlClient;

namespace BoostMyTool.Application.UseCase;


public record CreateClientRequest(string Name, string Email, string Phone, string Address) : IRequest<CreateClientResponse>
{
    public string GetCreatQuery() => "INSERT INTO clients (name, email, phone, address) VALUES (@name, @email, @phone, @address);";
}
public record CreateClientResponse(string Name, string Email, string Phone, string Address)
{
    public string RedirectPath() => "/Clients/Index";
}
public class CreateClientHandler : IRequestHandler<CreateClientRequest, CreateClientResponse>
{
    private readonly ISettings _settings;
    public CreateClientHandler(ISettings setting)
    {
        _settings = setting;
    }

    public async Task<CreateClientResponse> Handle(CreateClientRequest request, CancellationToken cancellationToken)
    {
        using (SqlConnection connection = new SqlConnection(_settings.GetDBConnectionInfo()))
        {
            connection.Open();
            using SqlCommand command = new(request.GetCreatQuery(), connection);
            command.Parameters.AddWithValue("@name", request.Name);
            command.Parameters.AddWithValue("@email", request.Email);
            command.Parameters.AddWithValue("@phone", request.Phone);
            command.Parameters.AddWithValue("@address", request.Address);
            command.ExecuteNonQuery();
        }

        return new CreateClientResponse("", "", "", "");
    }
}