using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RegexService.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            MatchResponse matchedPerson = MatchMaker.GetMeAGame(user);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}