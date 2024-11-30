using BoostMyTool.Application.Interfaces;
using BoostMyTool.Model;
using MediatR;
using System.Data.SqlClient;

namespace BoostMyTool.Application.UseCase;
public record GetClientFromIdRequest(string Id) : IRequest<GetClientFromIdResponse>
{
    public string GetQuery() => "SELECT * FROM clients WHERE id=@id";
}
public record GetClientFromIdResponse(ClientInfo ClientInfo);
public class GetClientFromIdHandler : IRequestHandler<GetClientFromIdRequest, GetClientFromIdResponse>
{
    private readonly ISettings _settings;
    public GetClientFromIdHandler(ISettings setting)
    {
        _settings = setting;
    }

    public async Task<GetClientFromIdResponse> Handle(GetClientFromIdRequest request, CancellationToken cancellationToken)
    {
        var client = new ClientInfo();

        using (SqlConnection connection = new SqlConnection(_settings.GetDBConnectionInfo()))
        {
            connection.Open();
            using SqlCommand command = new(request.GetQuery(), connection);
            command.Parameters.AddWithValue("@id", request.Id);
            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                client.Id = reader.GetInt32(0);
                client.Name = reader.GetString(1);
                client.Email = reader.GetString(2);
                client.Phone = reader.GetString(3);
                client.Address = reader.GetString(4);
            }
        }

        return new GetClientFromIdResponse(client);
    }
}