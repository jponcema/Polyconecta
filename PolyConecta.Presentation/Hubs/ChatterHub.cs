using Microsoft.AspNetCore.SignalR;

namespace PolyConecta.Presentation.Hubs;

public class ChatterHub : Hub
{
    public async Task SendMessage(string documentId, string author, string text)
    {
        await Clients.All.SendAsync("ReceiveChatterMessage", documentId, author, text, DateTime.UtcNow.ToString("g"));
    }
}
