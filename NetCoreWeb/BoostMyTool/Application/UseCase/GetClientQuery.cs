using BoostMyTool.Application.Interfaces;
using BoostMyTool.Model;
using MediatR;
using System.Data.SqlClient;

namespace BoostMyTool.Application.UseCase;

public record GetClientRequest() : IRequest<GetClientResponse>
{
    public string GetQuery() => "Select * from clients";
}
public record GetClientResponse(IEnumerable<ClientInfo> ClientInfos);
public class GetClientHandler : IRequestHandler<GetClientRequest, GetClientResponse>
{
    private readonly ISettings _settings;
    public GetClientHandler(ISettings setting)
    {
        _settings = setting;
    }

    public async Task<GetClientResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
    {
        var listClients = new List<ClientInfo>();
        try
        {
            using (SqlConnection connection = new SqlConnection(_settings.GetDBConnectionInfo()))
            {
                connection.Open();
                using SqlCommand command = new(request.GetQuery(), connection);
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
        return new GetClientResponse(listClients);
    }
}