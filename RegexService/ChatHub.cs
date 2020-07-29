using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
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
            string messageType = this.ParseMessage(message);
            if (messageType == "ping" || messageType == "queue")
            {
                connectionList.AddOrUpdate(user, Context.ConnectionId, (x, y) => Context.ConnectionId);
            }

            if (messageType == "queue")
            {
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
            if (messageType == "query" || messageType == "hint")
            {
                string matchedPlayer = MatchMaker.GetMatchedPlayer(user);
                await Clients.Client(connectionList[matchedPlayer]).SendAsync("ReceiveMessage", user, message);
            }
            
        }

        private string ParseMessage(string message)
        {
            var parsed = JObject.Parse(message);
            var type = parsed.GetValue("type");
            return type.ToString();
        }
    }
}