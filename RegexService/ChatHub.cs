using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RegexService.Hubs
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> connectionList = new ConcurrentDictionary<string, string>();
        public async Task SendMessage(string user, string message)
        {
            connectionList.AddOrUpdate(user, Context.ConnectionId, (x, y) => Context.ConnectionId);
            MatchResponse matchedPerson = MatchMaker.GetMeAGame(user);
            if (matchedPerson.matched)
            {
                System.Random r = new System.Random();
                if (r.Next(2) == 0)
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", matchedPerson.NameMatchedTo, "asker");
                    await Clients.Client(connectionList[matchedPerson.NameMatchedTo]).SendAsync("ReceiveMessage", user, "responder");
                }
                else
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", matchedPerson.NameMatchedTo, "responder");
                    await Clients.Client(connectionList[matchedPerson.NameMatchedTo]).SendAsync("ReceiveMessage", user, "asker");
                }
            }
        }
    }
}