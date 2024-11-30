using BoostMyTool.Application.Interfaces;

namespace BoostMyTool.Model;

public class ConnectionSettings(string dbConnection) : ISettings
{
    private readonly string _dbConnection = dbConnection;

    public string GetDBConnectionInfo() => _dbConnection;
}
