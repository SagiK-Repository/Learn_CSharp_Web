using BoostMyTool.Application.Interfaces;
using MediatR;
using System.Data.SqlClient;

namespace BoostMyTool.Application.UseCase;

public record DeleteClientRequest(string Id) : IRequest<DeleteClientResponse>
{
    public string GetQuery() => "DELETE FROM clients WHERE id=@id;";
}
public record DeleteClientResponse()
{
    public string GetReDirect() => "/Clients/Index";
}
public class DeleteClientHandler : IRequestHandler<DeleteClientRequest, DeleteClientResponse>
{
    private readonly ISettings _settings;
    public DeleteClientHandler(ISettings setting)
    {
        _settings = setting;
    }

    public async Task<DeleteClientResponse> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_settings.GetDBConnectionInfo()))
            {
                connection.Open();
                using SqlCommand command = new(request.GetQuery(), connection);
                command.Parameters.AddWithValue("@id", request.Id);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        { }

        return new DeleteClientResponse();
    }
}
