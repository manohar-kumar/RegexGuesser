using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

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

            if (messageType == "query")
            {
                JObject obj = JObject.Parse(message);
                string regexString = obj.GetValue("regexString").ToString();
                Regex regObj;

                try
                {
                    regObj = new Regex(regexString);
                }
                catch (Exception ex)
                {
                    JObject excobj = GetJObjectWithMessageType("exception");
                    excobj.Add("error", "Wrong Regex String. " + ex.Message);
                    await Clients.Caller.SendAsync("ReceiveMessage", user, excobj.ToString());
                    return;
                }

                List<string> matchStrings = obj.GetValue("matchString").ToObject<List<string>>();
                HashSet<string> allStrings = new HashSet<string>();
                foreach (string str in matchStrings)
                {
                    if (allStrings.Contains(str))
                    {
                        JObject excobj = GetJObjectWithMessageType("exception");
                        excobj.Add("error", "Please give different strings for helping the guesser.");
                        await Clients.Caller.SendAsync("ReceiveMessage", user, excobj.ToString());
                        return;
                    }
                    if (!regObj.IsMatch(str))
                    {
                        JObject excobj = GetJObjectWithMessageType("exception");
                        excobj.Add("error", "One of the strings do not match with the Regex");
                        await Clients.Caller.SendAsync("ReceiveMessage", user, excobj.ToString());
                        return;
                    }
                    allStrings.Add(str);
                }
                List<string> nomatchStrings = obj.GetValue("noMatchString").ToObject<List<string>>();
                foreach (string str in nomatchStrings)
                {
                    if (allStrings.Contains(str))
                    {
                        JObject excobj = GetJObjectWithMessageType("exception");
                        excobj.Add("error", "Please give different strings for helping the guesser.");
                        await Clients.Caller.SendAsync("ReceiveMessage", user, excobj.ToString());
                        return;
                    }

                    if (regObj.IsMatch(str))
                    {
                        JObject excobj = GetJObjectWithMessageType("exception");
                        excobj.Add("error", "One of the strings matches with the Regex but should not match");
                        await Clients.Caller.SendAsync("ReceiveMessage", user, excobj.ToString());
                        return;
                    }
                    allStrings.Add(str);
                }
            }

            if (messageType == "query" || messageType == "hint" || messageType == "solved")
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