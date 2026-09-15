using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Contpaq.Bridge.Api.Hubs
{
    public class DashboardHub : Hub
    {
        public async Task JoinDashboardGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "DashboardWatchers");
        }
    }
}
