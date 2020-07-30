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

            if (messageType == "queue" && connectionList.ContainsKey(user))
            {
                JObject obj = GetJObjectWithMessageType("NameApproval");
                obj.Add("approval", "false");
                await Clients.Caller.SendAsync("ReceiveMessage", user, obj.ToString());
                return;
            }
            else
            {
                JObject obj = GetJObjectWithMessageType("NameApproval");
                obj.Add("approval", "true");
                await Clients.Caller.SendAsync("ReceiveMessage", user, obj.ToString());
                await Task.Delay(1000);
            }

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
                    JObject askerobj = GetJObjectWithMessageType("queue");
                    JObject responderobj = GetJObjectWithMessageType("queue");

                    askerobj.Add("playerType", "asker");
                    responderobj.Add("playerType", "responder");

                    if (r.Next(2) == 0)
                    {
                        await Clients.Caller.SendAsync("ReceiveMessage", matchedPerson.NameMatchedTo, askerobj.ToString());
                        await Clients.Client(connectionList[matchedPerson.NameMatchedTo]).SendAsync("ReceiveMessage", user, responderobj.ToString());
                    }
                    else
                    {
                        await Clients.Caller.SendAsync("ReceiveMessage", matchedPerson.NameMatchedTo, responderobj.ToString());
                        await Clients.Client(connectionList[matchedPerson.NameMatchedTo]).SendAsync("ReceiveMessage", user, askerobj.ToString());
                    }
                }
            }
            if (messageType == "query" || messageType == "hint")
            {
                string matchedPlayer = MatchMaker.GetMatchedPlayer(user);
                await Clients.Client(connectionList[matchedPlayer]).SendAsync("ReceiveMessage", user, message);
            }
            
        }

        private JObject GetJObjectWithMessageType(string messageType)
        {
            JObject obj = new JObject();
            obj.Add("type", messageType);
            return obj;
        }

        private string ParseMessage(string message)
        {
            var parsed = JObject.Parse(message);
            var type = parsed.GetValue("type");
            return type.ToString();
        }
    }
}