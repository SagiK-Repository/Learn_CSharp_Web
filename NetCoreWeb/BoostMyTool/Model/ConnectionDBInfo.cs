namespace BoostMyTool.Model;

public class ConnectionDBInfo(string dBConnection)
{
    public string DBConnection { get; private set; } = dBConnection;
}
