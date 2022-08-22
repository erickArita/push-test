using Microsoft.AspNetCore.SignalR;

public class MainHub : Hub
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            Console.WriteLine("Streaming");
            yield return DateTime.UtcNow;
            await Task.Delay(1000, cancellationToken);
        }
    }
}