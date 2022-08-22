namespace MainSignalServer.Models;

public class RequestSubs
{
    public string Endpoint { get; set; }
    public string User { get; set; }
    public Keys keys { get; set; }
}

public class Keys
{
    public string P256dh { get; set; }
    public string auth { get; set; }
}