﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegexService
{
    public class MatchResponse
    {
        public string NameMatchedTo;
        public bool matched;
    }

    public class MatchMaker
    {
        private static object lockobject = new object();

        private static readonly Lazy<MatchMaker> lazy = new Lazy<MatchMaker>(() => new MatchMaker());

        public static MatchMaker Instance { get { return lazy.Value; } }

        private static Queue<string> AllUsersWaiting = new Queue<string>();

        private static Dictionary<string, string> matchedPlayers = new Dictionary<string, string>();

        public static MatchResponse GetMeAGame(string name)
        {
            lock (lockobject)
            {
                if (AllUsersWaiting.Count == 0)
                {
                    AllUsersWaiting.Enqueue(name);
                    return new MatchResponse
                    {
                        matched = false
                    };
                }
                else
                {
                    string matchedPerson = AllUsersWaiting.Dequeue();
                    matchedPlayers.Add(matchedPerson, name);
                    matchedPlayers.Add(name, matchedPerson);
                    return new MatchResponse
                    {
                        matched = true,
                        NameMatchedTo = matchedPerson
                    };
                }
            }
        }

        public static string GetMatchedPlayer(string user)
        {
            return matchedPlayers[user];
        }
    }
}
