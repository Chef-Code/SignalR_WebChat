using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Models.DataModels
{
    public class Team
    {
        public Team() { }
        public Team(string Name, string Hash) : this()
        {
            this.Name = Name;
            this.Hash = Hash;
            Members = new List<AppUser>();
            this.TeamId = Guid.NewGuid().ToString("d");

            if (Members.Count > 0)
            {
                Members[0].IsPlaying = IsPlaying;
            }

            if (Members.Count >1)
            {
                Members[1].IsPlaying = IsPlaying;
            }
        }
        public string TeamId { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public List<AppUser> Members { get; set; }
        public bool IsPlaying { get; set; }
        public int Score { get; set; }

    }
}