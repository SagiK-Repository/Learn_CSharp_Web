using BoostMyTool.Application.Interfaces;
using BoostMyTool.Model;
using MediatR;
using System.Data.SqlClient;

namespace BoostMyTool.Application.UseCase;

public record UpdateClientRequest(ClientInfo UpdateClient) : IRequest<UpdateClientResponse>
{
    public string GetQuery() => "UPDATE clients " +
                                "SET name=@name, email=@email, phone=@phone, address=@address " +
                                "WHERE id=@id;";
}
public record UpdateClientResponse()
{
    public string GetRedirect() => "/Clients/Index";
}
public class UpdateClientHandler : IRequestHandler<UpdateClientRequest, UpdateClientResponse>
{
    private readonly ISettings _settings;
    public UpdateClientHandler(ISettings setting)
    {
        _settings = setting;
    }

    public async Task<UpdateClientResponse> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
    {
        using (SqlConnection connection = new SqlConnection(_settings.GetDBConnectionInfo()))
        {
            connection.Open();

            using SqlCommand command = new(request.GetQuery(), connection);
            command.Parameters.AddWithValue("@name", request.UpdateClient.Name);
            command.Parameters.AddWithValue("@email", request.UpdateClient.Email);
            command.Parameters.AddWithValue("@phone", request.UpdateClient.Phone);
            command.Parameters.AddWithValue("@address", request.UpdateClient.Address);
            command.Parameters.AddWithValue("@id", request.UpdateClient.Id);
            command.ExecuteNonQuery();
        }

        return new UpdateClientResponse();
    }
}