using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR_WebChat.Models.DataModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SignalR_WebChat
{
    public class GameState
    {
        //Singleton instance
        private readonly static Lazy<GameState> _instance = new Lazy<GameState>(() => 
        new GameState(
            GlobalHost.ConnectionManager.GetHubContext<GameHub>()
            ));

        private readonly ConcurrentDictionary<string, AppUser> _players = new ConcurrentDictionary<string, AppUser>(StringComparer.OrdinalIgnoreCase);

        private readonly ConcurrentDictionary<string, Team> _teams = new ConcurrentDictionary<string, Team>(StringComparer.OrdinalIgnoreCase);

        private readonly ConcurrentDictionary<string, Game> _games = new ConcurrentDictionary<string, Game>(StringComparer.OrdinalIgnoreCase);

        private GameState(IHubContext context)
        {
            Clients = context.Clients;
            Groups = context.Groups;
        }

        public static GameState Instance
        {
            get { return _instance.Value; }
        }

        public IHubConnectionContext<dynamic> Clients { get; set; }
        public IGroupManager Groups { get; set; }

        public AppUser CreatePlayer(string alias)
        {
            //TODO: Add a NAME_ALREADY_EXSISTS check on alias against _players
            var player = new AppUser(alias, GetMD5Hash(alias));
            //_players[alias] = player;
            _players[player.AppUserId] = player;
            return player;
        }

        private string GetMD5Hash(string alias)
        {
            return String.Join("", MD5.Create()
                .ComputeHash(Encoding.Default.GetBytes(alias))
                .Select(b => b.ToString("x2")));           
        }

        public AppUser GetPlayer(string alias)
        {
            return _players.Values.FirstOrDefault(u => u.Alias == alias);
        }
        public Suit GetSuit(string NameOfSuit)
        {
            Suit suit = new Clubs();
            switch(NameOfSuit)
            {
                case "clubs":
                    suit = new Clubs();
                    break;
                case "diamonds":
                    suit = new Diamonds();
                    break;
                case "hearts":
                    suit = new Hearts();
                    break;
                case "spades":
                    suit = new Spades();
                    break;
                default:
                    throw new NullReferenceException("Suit must be one of the following; clubs, diamonds, hearts or spades.");
                    break;
            }
            return suit;
        }
        public Team InitializeTeam(AppUser appUser, string teamName)
        {
            //TODO: Add a NAME_ALREADY_EXSISTS check on teamName against _teams 
            var team = new Team(teamName, GetMD5Hash(teamName));

            team.Members.Add(appUser);
            Groups.Add(appUser.ConnectionId, team.TeamId);
            appUser.HasTeam = true;

            //_teams[teamName] = team;
            _teams[team.TeamId] = team;
            return team;
        }
        public Game FindGameByTeams(Team team1, Team team2)
        {
            var game = _games.Values.FirstOrDefault(g => g.Team1.TeamId == team1.TeamId && g.Team2.TeamId == team2.TeamId);
            return game;
        }
        public Game FindGameByGameId(string gameId)
        {
            var game = _games.Values.FirstOrDefault(g => g.GameId == gameId);
            return game;
        }
        public Game FindGameByGame(Game Game)
        {
            var game = _games.Values.FirstOrDefault(g => g.GameId == Game.GameId);
            return game;
        }
        public Team FindTeamByMember(AppUser appUser)
        {
            var team = _teams.Values.FirstOrDefault(t => t.Members[0].AppUserId == appUser.AppUserId || t.Members[1].AppUserId == appUser.AppUserId);
            return team;
        }
        public Team FindTeamByTeamId(string teamId)
        {
            var teamFound = _teams.Values.FirstOrDefault(t => t.TeamId == teamId);
            return teamFound;
        }
        public List<Team> GetAllSingleMemberTeams()
        {
            var singleMemberTeams = _teams.Values.Where(t => t.Members.Count == 1).ToList();
            
            return singleMemberTeams;
        }
        public List<Team> GetAllFullMemberTeams()
        {
            var fullMemberTeams = _teams.Values.Where(t => t.Members.Count == 2).ToList();

            return fullMemberTeams;
        }
        public List<Team> GetListTeamsNotPlaying()
        {
            var teamsNotPlaying = _teams.Values.Where(t => t.IsPlaying == false).ToList();

            return teamsNotPlaying;
        }
        public Team GetFirstTeamNotPlaying(Team team)
        {
            var firstTeamNotPlaying = _teams.Values.FirstOrDefault(t => t.IsPlaying == false && t.TeamId != team.TeamId && t.Members.Count == 2);

            return firstTeamNotPlaying;
        }
        public Team GetSingleMemberTeam()
        {
            var singleMemberTeam = _teams.Values.FirstOrDefault(t => t.Members.Count == 1);

            return singleMemberTeam;
        }
        public bool TryAddToPreexistingTeam(Team targetTeam, AppUser appUser)
        {
            //Team team;
            //_teams.TryGetValue(targetTeam.TeamId, out team);
            var team = FindTeamByTeamId(targetTeam.TeamId);
            if(team != null)
            {
                if(team.Members.Count == 1)
                {
                    team.Members.Add(appUser);
                    appUser.HasTeam = true;
                    return true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(team.TeamId, "This team has too many members");
                }

            }

            return false;
        }
        public Team CreateTeam(List<AppUser> appUsers, string teamName)
        {
            var team = new Team(teamName, GetMD5Hash(teamName));

            foreach (var appUser in appUsers)
            {
                team.Members.Add(appUser);
                Groups.Add(appUser.ConnectionId, team.TeamId);
            }

            //_teams[teamName] = team;
            _teams[team.TeamId] = team;
            return team;
        }
        public AppUser GetNewOpponent(List<AppUser> appUsers)
        {
            var potential = _players.Values.Except(appUsers).ToList();

            return potential.FirstOrDefault();
        }

        public List<AppUser> GetNewOpponents(List<AppUser> appUsers)
        {
            List<AppUser> candidates = new List<AppUser>();
            foreach(var appUser in appUsers)
            {
                candidates.Add(_players.Values.FirstOrDefault(u => u.IsPlaying == false && u.AppUserId != appUser.AppUserId));
            }

            return candidates.Take(2).ToList();
        }
        public AppUser GetNewTeamMate(AppUser appUser)
        {
            return _players.Values.FirstOrDefault(u => u.IsPlaying == false && u.HasTeam == false && u.AppUserId != appUser.AppUserId);
        }

        public AppUser GetTeamMate(AppUser appUser, Game game)
        {
            if (game.Player1.AppUserId == appUser.AppUserId)
            {
                return game.Player3;
            }
            else if (game.Player2.AppUserId == appUser.AppUserId)
            {
                return game.Player4;
            }
            else if (game.Player3.AppUserId == appUser.AppUserId)
            {
                return game.Player1;
            }
            else //(game.Player4.AppUserId == appUser.AppUserId)
            {
                return game.Player2;
            }

        }
        /*public List<AppUser> GetOpponents(AppUser appUser, Game game)
        {
            List<AppUser> opponents = new List<AppUser>();

            if(game.Player1.AppUserId == appUser.AppUserId || game.Player3.AppUserId == appUser.AppUserId)
            {
                opponents.Add(game.Player2);
                opponents.Add(game.Player4);
                return opponents;
            }
            else //(game.Player2.AppUserId == appUser.AppUserId || game.Player4.AppUserId == appUser.AppUserId)
            {
                opponents.Add(game.Player1);
                opponents.Add(game.Player3);
                return opponents;
            }

        }*/
        //....

        public Game CreateGame(AppUser player, List<AppUser> opponents, AppUser teamMate)
        {
            
            var game = new Game()
            {
                Player1 = player,
                Player2 = opponents[0],
                Player3 = teamMate,
                Player4 = opponents[1],

                GameId = Guid.NewGuid().ToString("d")
            };
             
            _games[game.GameId] = game;

            player.IsPlaying = true;
            opponents[0].IsPlaying = true;
            teamMate.IsPlaying = true;
            opponents[1].IsPlaying = true;

            Groups.Add(player.ConnectionId, game.GameId);
            Groups.Add(opponents[0].ConnectionId, game.GameId);
            Groups.Add(teamMate.ConnectionId, game.GameId);
            Groups.Add(opponents[1].ConnectionId, game.GameId);

            return game;
        }
        public Game CreateGame(Team team1, Team team2)
        {

            var game = new Game()
            {
                Team1 = team1,
                Team2 = team2,
                Player1 = team1.Members[0],
                Player2 = team2.Members[0],
                Player3 = team1.Members[1],
                Player4 = team2.Members[1],

                GameId = Guid.NewGuid().ToString("d")
           };

            //Both teams
            var gameGuid = game.GameId;
            _games[gameGuid] = game;

            Groups.Add(team1.Members[0].ConnectionId, gameGuid);
            Groups.Add(team1.Members[1].ConnectionId, gameGuid);
            Groups.Add(team2.Members[0].ConnectionId, gameGuid);
            Groups.Add(team2.Members[1].ConnectionId, gameGuid);

            //team1
            team1.IsPlaying = true;
            //team2
            team2.IsPlaying = true;

            return game;
        }
        public Team BuildTeam(List<AppUser> appUsers, string teamName)
        {
            var team = new Team(teamName, GetMD5Hash(teamName));

            team.Members.AddRange(appUsers);

            var teamGuid = Guid.NewGuid().ToString("d");
            _teams[teamGuid] = team;

            team.Members[0].IsPlaying = true;
            team.Members[1].IsPlaying = true;

            Groups.Add(team.Members[0].ConnectionId, teamGuid);
            Groups.Add(team.Members[1].ConnectionId, teamGuid);

            return team;
        }

        public Team FindTeam(AppUser appUser, out AppUser teamMate)
        {
            teamMate = null;
            if(appUser.Group == null)
            {
                return null;
            }

            Team team;
            _teams.TryGetValue(appUser.Group, out team);

            if(team != null)
            {
                //could expand this method right here... by dynamically checking Members.Count for GAMES that require teams > 2
                if (team.Members.Count == 2)
                {
                    if (appUser.AppUserId == team.Members[0].AppUserId)
                    {
                        teamMate = team.Members[1];
                        return team;
                    }
                    else
                    {
                        teamMate = team.Members[0];
                        return team;
                    }
                }
                
                //for now it just assumes the team *only has 1... if it gets this far
                if(team.Members.Count == 1)
                {
                    if (appUser.AppUserId == team.Members[0].AppUserId)
                    {
                        teamMate = team.Members[1];
                        return team;
                    }
                    if (appUser.AppUserId == team.Members[1].AppUserId)
                    {
                        teamMate = team.Members[0];
                        return team;
                    }
                    else
                    {
                        teamMate = null;
                        return team;
                    }
                }
                else
                {
                    //the team has more than 2
                    throw new ArgumentOutOfRangeException();
                }
            }
            return null;
        }
        public Game FindGame(Team team1, out Team team2)
        {
            team2 = null;

            if (team1.Members[0].Group == null)
                return null;

            Game game;
            _games.TryGetValue(team1.Members[0].Group, out game);

            if(game != null)
            {
                if (team1.TeamId == game.Team1.TeamId)
                {
                    team2 = game.Team2;
                    return game;
                }
                else
                {
                    team2 = game.Team1;
                    return game;
                }
            }
            return null;
        }
        //this needs tested and most likely refactored, logic is questionable to what it is suppose to accomplish
        public Game FindGame(AppUser player, out Team team1, out Team team2)
        {
            team1 = null;
            team2 = null;
            /*
            if (player.Group == null)
                return null;

            Game game;
            _games.TryGetValue(player.Group, out game);

            if(game != null)
            {
                if(player.AppUserId == game.Player1.AppUserId)
                {
                    opponents.Add(game.Player2);
                    opponents.Add(game.Player4);
                    teamMate = game.Player3;
                    return game;
                }
                else if (player.AppUserId == game.Player3.AppUserId)
                {
                    opponents.Add(game.Player2);
                    opponents.Add(game.Player4);
                    teamMate = game.Player1;
                    return game;
                }
                else if (player.AppUserId == game.Player2.AppUserId)
                {
                    opponents.Add(game.Player1);
                    opponents.Add(game.Player3);
                    teamMate = game.Player4;
                    return game;
                }
                else //player.AppUserId == game.Player4.AppUserId
                {
                    opponents.Add(game.Player1);
                    opponents.Add(game.Player3);
                    teamMate = game.Player2;
                    return game;
                }
            }*/
            return null;
        }
        //needs to include, resetting the _teams
        public void ResetGame(Game game)
        {
            var groupName = game.Player1.Group;

            var player1Name = game.Player1.Alias;
            var player2Name = game.Player2.Alias;
            var player3Name = game.Player3.Alias;
            var player4Name = game.Player4.Alias;

            Groups.Remove(game.Player1.ConnectionId, groupName);
            Groups.Remove(game.Player2.ConnectionId, groupName);
            Groups.Remove(game.Player3.ConnectionId, groupName);
            Groups.Remove(game.Player4.ConnectionId, groupName);

            AppUser p1;
            _players.TryRemove(player1Name, out p1);

            AppUser p2;
            _players.TryRemove(player2Name, out p2);

            AppUser p3;
            _players.TryRemove(player3Name, out p3);

            AppUser p4;
            _players.TryRemove(player4Name, out p4);

            Game g;
            _games.TryRemove(groupName, out g);
        }
    }
}