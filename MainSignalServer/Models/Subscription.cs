namespace MainSignalServer.Models;

public class SubRequest
{
    public int Id { get; set; }

    public string Endpoint { get; set; }
    public string User { get; set; }
    public string P256dh { get; set; }
    public string auth { get; set; }
}