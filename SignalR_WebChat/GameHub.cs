using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalR_WebChat.Models.DataModels;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace SignalR_WebChat
{
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        public override Task OnConnected()
        {
            //TODO: limit the payload transfered over the wire to the client (i.e. linq.Take(10), or only send TeamId's)
            var singleTeams = GameState.Instance.GetAllSingleMemberTeams();
            var fullTeams = GameState.Instance.GetAllFullMemberTeams();
            if(singleTeams.Count > 0)
            {
                Clients.Caller.updateTeamsWaitingList(singleTeams);
            }
            if (fullTeams.Count > 0)
            {
                Clients.Caller.updateTeamsWaitingList(fullTeams);
            }
            //TODO: update this clients friends list, alert this client has connected



            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                //Usecase: Client ends a match or purposefully quits
                Console.WriteLine($"Client {Context.ConnectionId} explicitly closed the connection.");
            }
            else
            {
                Console.WriteLine($"Client {Context.ConnectionId} timed out .");
            }

            return base.OnDisconnected(stopCalled);
        }
         
        public override Task OnReconnected()
        {

            return base.OnReconnected();
        }

        public bool JoinTeam(string alias, string teamId)
        {
            var player = GameState.Instance.GetPlayer(alias);
            if (player != null)
            {
                Clients.Caller.playerExists();
                return true;
            }
            player = GameState.Instance.CreatePlayer(alias);
            player.ConnectionId = Context.ConnectionId;
            Clients.Caller.alias = player.Alias;
            Clients.Caller.hash = player.Hash;
            Clients.Caller.AppUserId = player.AppUserId;

            var team = GameState.Instance.FindTeamByTeamId(teamId);

            var joinedTeam = GameState.Instance.TryAddToPreexistingTeam(team, player);

            if(!joinedTeam)
            {
                Clients.Caller.joinTeamFailure(team, player);
            }

            Clients.Caller.playerJoined(player);

            return StartGameBeta(team);
        }

        public bool CreateNewTeam(string alias, string teamName)
        {
            var player = GameState.Instance.GetPlayer(alias);
            if (player != null)
            {
                Clients.Caller.playerExists();
                return true;
            }
                player = GameState.Instance.CreatePlayer(alias);
                player.ConnectionId = Context.ConnectionId;
                Clients.Caller.alias = player.Alias;
                Clients.Caller.hash = player.Hash;
                Clients.Caller.AppUserId = player.AppUserId;

                //TODO... Join Team
                var team = GameState.Instance.InitializeTeam(player, teamName);
                Clients.Caller.playerJoined(player);

            return StartGameBeta(team);
        }

        private bool StartGameBeta(Team team)
        {
            if(team != null)
            {
                Team team2;

                if(team.Members.Count == 1)
                {
                    AppUser teamMate;
                    teamMate = GameState.Instance.GetNewTeamMate(team.Members[0]);
                    if(teamMate != null)
                    {
                       GameState.Instance.TryAddToPreexistingTeam(team, teamMate);

                       //Clients.Caller.removeTeamFromWaitingList(team);
                    }

                    Clients.All.teamWaitingList(team);
                    return true;
                }

                if(team.Members.Count >= 2)
                {
                    var game = GameState.Instance.FindGame(team, out team2);
                    if (game != null)
                    {
                        //TODO: adjust client 

                        Clients.Group(game.GameId).buildBoard(game);
                        return true;
                    }

                    team2 = GameState.Instance.GetFirstTeamNotPlaying(team);

                    if (team2 == null)
                    {
                        Clients.All.removeTeamFromWaitingList(team);
                        Clients.All.teamWaitingList(team);
                        return true;
                    }

                    game = GameState.Instance.CreateGame(team, team2);

                    Clients.All.removeTeamFromWaitingList(team);
                    Clients.All.teamWaitingList(team);

                    Random rnd = new Random();
                    int startingDealer = rnd.Next(1, 5);
                    if (startingDealer == 1)
                    {
                        game.Player1.IsDealer = true;
                    }
                    if (startingDealer == 2)
                    {
                        game.Player2.IsDealer = true;
                    }
                    if (startingDealer == 3)
                    {
                        game.Player3.IsDealer = true;
                    }
                    if (startingDealer == 4)
                    {
                        game.Player4.IsDealer = true;
                    }

                    var dealer = new PinochleDealer();
                    dealer.CutCards();
                    dealer.RiffleCards();
                    dealer.Shuffle();

                    for (int i = 0; i < 5; i++)
                    {
                        game.Player1.PinochleHand.Cards.AddRange(dealer.Deal(4));
                        game.Player2.PinochleHand.Cards.AddRange(dealer.Deal(4));
                        game.Player3.PinochleHand.Cards.AddRange(dealer.Deal(4));
                        game.Player4.PinochleHand.Cards.AddRange(dealer.Deal(4));
                    }

                    Clients.Group(game.GameId).buildBoard(game); 
                    return true;
                }
            }
            return false;
        }

        #region OLD StartGame(AppUser player) method
        private bool StartGame(AppUser player)
        {
            if(player != null)
            {
                /*Team teamMate;
                AppUser opponent1;
                AppUser opponent2;

                Team opponents;
                List<AppUser> team1 = new List<AppUser>();

                List<AppUser> team2 = new List<AppUser>();
                List<AppUser> team1plus1 = new List<AppUser>();


                var game = GameState.Instance.FindGame(player, out opponents, out teamMate);
                if(game != null)
                {
                    Clients.Group(player.Group).buildBoard(game);
                }

                teamMate = GameState.Instance.GetNewTeamMate(player);

                if(team1.Count < 2)
                {                    

                    if (!team1.Contains(player) && player != null)
                    {
                        team1.Insert(0, player);
                    }
                    if (!team1.Contains(teamMate) && teamMate != null)
                    {
                        team1.Insert(1, teamMate);
                    }
                }

                if(team1.Count == 2)
                {
                    opponent1 = GameState.Instance.GetNewOpponent(team1);

                    if (opponent1 != null)
                    {
                        team1plus1.AddRange(team1);
                        team1plus1.Add(opponent1);
                    }

                    if (team1plus1.Count == 3)
                    {
                        opponent2 = GameState.Instance.GetNewOpponent(team1plus1);

                        if (opponent2 != null)
                        {
                            if (!team2.Contains(opponent1))
                            {
                                team2.Insert(0, opponent1);
                            }
                            if (!team2.Contains(opponent2))
                            {
                                team2.Insert(1, opponent2);
                            }
                        }
                    }
                }

                if(team1.Count != 2 || team2.Count != 2)
                {
                    Clients.Caller.waitingList();                 
                    return true;
                }


                game = GameState.Instance.CreateGame(team1[0], team2, team1[1]);
                //Clients.Group(player.Group).dealCards(game);  <-- This is most likely what I will have TODO:
                Clients.Group(player.Group).buildBoard(game);
                return true;


            */
            }
            return false;
        }
        #endregion
        public bool SubmitBid(int bid, string gameId)
        {
            var userAlias = Clients.Caller.alias;
            var player = GameState.Instance.GetPlayer(userAlias);  //TODO: rewrite this method, it does not need to check for all these Game Instances
            if (player != null)
            {
                var game = GameState.Instance.FindGameByGameId(gameId);
                if(game != null)
                {
                    player.Bid = bid;
                    var lastToBid = player.AppUserId;
                    Clients.Group(game.GameId).updateCurrentBid(bid, game, lastToBid);
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool PassBid(int bid, string gameId)
        {
            var userAlias = Clients.Caller.alias;
            var player = GameState.Instance.GetPlayer(userAlias);  //TODO: rewrite this method, it does not need to check for all these Game Instances
            if (player != null)
            {
                var game = GameState.Instance.FindGameByGameId(gameId);
                if (game != null)
                {
                    player.PassBid = true;
                    var lastToBid = player.AppUserId;
                    Clients.Group(game.GameId).updateCurrentBid(bid, game, lastToBid);
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool DeclareTrumpSuit(string nameOfSuit, string gameId)
        {
            var userAlias = Clients.Caller.alias;
            var player = GameState.Instance.GetPlayer(userAlias);  //TODO: rewrite this method, it does not need to check for all these Game Instances
            if (player != null)
            {
                var game = GameState.Instance.FindGameByGameId(gameId);
                if (game != null)
                {
                    var suit = GameState.Instance.GetSuit(nameOfSuit);

                    var p1 = game.Player1;
                    var p2 = game.Player2;
                    var p3 = game.Player3;
                    var p4 = game.Player4;
                    var allPlayers = new List<AppUser>() { p1, p2, p3, p4 };

                    foreach(var p in allPlayers)
                    {
                        p.DeclareTrumpSuit(suit);
                        p.MeldScore = new Meld(p.PinochleHand).GetScore();
                    }

                    Clients.Group(gameId).evaluateMeld(suit, game);
                    return true;
                }
            }           
            return false;
        }
        public bool PlayCard(string cardName)
        {
            var userAlias = Clients.Caller.alias;
            var player = GameState.Instance.GetPlayer(userAlias);
            if(player != null)
            {
                Team team1;
                Team team2;

                var game = GameState.Instance.FindGame(player, out team1, out team2);
                if(game != null)
                {
                    if(!string.IsNullOrEmpty(game.WhosTurn) && game.WhosTurn != player.Id)
                    {
                        return true;
                    }

                    var card = FindCard(game, cardName); // <-- my game logic will be different
                    Clients.Group(player.group).flipCard(card);
                    return true;
                }
            }
                return false;

        }

        private Card FindCard(Game game, string cardName)
        {
            //return game.Board.Pieces.FirstOrDefault(c => c.Name == cardName);
            return new Card();
        }

       /* public bool CheckCard(string cardName)
        {
            var userAlias = Clients.Caller.alias;
            AppUser player = GameState.Instance.GetPlayer(userAlias);
            if(player != null)
            {
                List<AppUser> playerOpponents;
                AppUser playerTeamMate;
                Game game = GameState.Instance.FindGame(player, out playerOpponents, out playerTeamMate);
                if(game != null)
                {
                    if (!string.IsNullOrEmpty(game.WhosTurn) && game.WhosTurn != player.Id)
                    {
                        return true;
                    }
                    Card card = FindCard(game, cardName);

                    if (game.LastCard == null)
                    {
                        game.WhosTurn = player.AppUserId;
                        game.LastCard = card;
                        return true;
                    }

                    //second flip

                    bool isMatch = IsMatch(game, card);
                    if (isMatch)
                    {
                        StoreMatch(player, card);
                        game.LastCard = null;
                        Clients.Group(player.Group).showMatch(card, userAlias);

                        if(player.Matches.Count >= 16)
                        {
                            Clients.Group(player.Group).winner(card, userAlias);
                            GameState.Instance.ResetGame(game);
                            return true;
                        }
                        return true;
                    }


                }
            }
            return false;
        }

        private bool GetIsMatch(Game game, Card card)
        {
            if(card == null)
            {
                return false;
            }

            if(game.LastCard != null)
            {
                if(game.LastCard.Pair == card.CardId)
                {
                    return true;
                }
                return false;
            }
            return false;
        }*/

    }
}