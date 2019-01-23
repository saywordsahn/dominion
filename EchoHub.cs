using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DominionWeb.Game;
using DominionWeb.Game.Common;
using Microsoft.AspNetCore.SignalR;
using DominionWeb.Game.Supply;
using Microsoft.AspNetCore.Authorization;
using DominionWeb.Game.Player;
using Microsoft.EntityFrameworkCore;
using DominionWeb.Game.Utils;

namespace DominionWeb
{
	[Authorize]
	public class EchoHub : Hub
	{
		private readonly Models.DominionContext _context;

		public EchoHub(
			Models.DominionContext context)
		{
			_context = context;
		}

        public async Task CreateLobby(string lobbyName)
        {
            var userId = _context.Users.Single(x => x.UserName == Context.UserIdentifier).Id;

            var userInLobby = _context.LobbyUser.Any(x => x.UserId == userId);

            if (userInLobby) {
                await Clients.User(Context.UserIdentifier).SendAsync("ReceiveSystemMessage", "You cannot create a lobby while you are already in one.");
                return;
            }

            var lobbyUser = new Models.LobbyUser
            {
                UserId = userId
            };

            var lobby = new Models.Lobby()
            {
                Name = lobbyName,
                HostId = userId,
                LobbyUser = new List<Models.LobbyUser> {
                    lobbyUser
                }
            };

            _context.Lobby.Add(lobby);

            await _context.SaveChangesAsync();

            var lobbies = _context.Lobby.ToList();

            await Clients.All.SendAsync("LobbyCreated", lobbies);
            //await Clients.User(Context.UserIdentifier).SendAsync("JoinLobby", Context.UserIdentifier, lobby);
        }

        //TODO: make restful instead of socket
        public async Task GetLobbies()
        {
            var lobbies = _context.Lobby;
            await Clients.All.SendAsync("LobbiesUpdated", lobbies);
        }

        public async Task JoinLobby(int lobbyId)
        {

            var lobby = _context.Lobby.Find(lobbyId);
            var userId = _context.Users.Single(x => x.UserName == Context.UserIdentifier).Id;

            var userInLobby = _context.LobbyUser.Any(x => x.UserId == userId);

            if (userInLobby) {
                await Clients.User(Context.UserIdentifier).SendAsync("ReceiveSystemMessage", "You cannot join another lobby while you are already in one.");
                return;
            }

            lobby.LobbyUser.Add(new Models.LobbyUser() { UserId = userId });
            await _context.SaveChangesAsync();

            var lobbies = _context.Lobby.Include(x => x.LobbyUser);
            await Clients.All.SendAsync("LobbiesUpdated", lobbies);
            //await Clients.All.SendAsync("JoinLobby", Context.UserIdentifier, lobby);
        }

        public async Task LeaveLobby(int lobbyId)
        {
            var lobby = _context.Lobby.Find(lobbyId);
            var userId = _context.Users.Single(x => x.UserName == Context.UserIdentifier).Id;

            var userInLobby = _context.LobbyUser.Single(x => x.LobbyId == lobby.LobbyId && x.UserId == userId);

            _context.LobbyUser.Remove(userInLobby);
            await _context.SaveChangesAsync();

            var remainingUsers = _context.LobbyUser.Where(x => x.LobbyId == lobby.LobbyId).ToList();

            if (remainingUsers.Count == 0) {
                _context.Lobby.Remove(_context.Lobby.Find(lobbyId));
            }

            await _context.SaveChangesAsync();
            
            var lobbies = _context.Lobby.ToList();

            await Clients.All.SendAsync("LobbiesUpdated", lobbies);
        }

        //eventually StartGame will replace newGame
        public void StartGame(int lobbyId)
        {

            var lobby = _context.Lobby.Include(x => x.LobbyUser)
                                .Single(x => x.LobbyId == lobbyId);

            var gameModel = new Models.Game()
            {
                DateTime = DateTime.Now
            };

            _context.Game.Add(gameModel);
            _context.SaveChanges();

            ISupplyFactory supplyFactory;

            supplyFactory = new RandomizedSupplyFactory();

            var userIds = lobby.LobbyUser.Select(x => x.UserId).ToList();
           

            var supply = supplyFactory.Create(userIds.Count);

            userIds.Shuffle();

            var players = new List<IPlayer>();
            for (int i = 0; i < userIds.Count; i++)
            {
                var userName = _context.Users.Find(userIds[i]).UserName;
                players.Add(new Player(i + 1, userName));
            }

            var game = new Game.Game(gameModel.GameId, players, supply, new VictoryCondition());

            game.Initialize();

            var gameState = new Models.GameState()
            {
                GameId = game.GameId,
                State = game.GetGameState()
            };

            _context.GameState.Add(gameState);
            _context.SaveChanges();

            //you're going to configure your client app to listen for this
            Clients.All.SendAsync("Send", supply);
            Clients.All.SendAsync("Game", game.GameId);
            foreach (var player in players) {
                Clients.User(player.PlayerName).SendAsync("GoToGame", game.GameId);
                Clients.User(player.PlayerName).SendAsync("Player", player);
            }
        }

        //TODO: rename echo hub or move logic to GameHub
        //TODO: refactor game startup as builder or factory
        //TODO: performance add homogenous pile class for most piles
        //you're going to invoke this method from the client app
        public void NewGame(bool randomizedKingdom)
		{
			var gameModel = new Models.Game()
			{
				DateTime = DateTime.Now
			};

			_context.Game.Add(gameModel);
			_context.SaveChanges();

			ISupplyFactory supplyFactory;
			
			if (randomizedKingdom)
			{
				supplyFactory = new RandomizedSupplyFactory();
			}
			else
			{
				supplyFactory = new DefaultSupplyFactory();
			}
				
			var supply = supplyFactory.Create(2);

			var ben = new Player(1, "ben@gmail.com");
			var maria = new Player(2, "maria@gmail.com");

			var players = new List<IPlayer>() {ben, maria};
			
			var defaultVictoryCondition = new VictoryCondition();

			var game = new Game.Game(gameModel.GameId, players, supply, defaultVictoryCondition);

			game.Initialize();

			var gameState = new Models.GameState()
			{
				GameId = game.GameId,
				State = game.GetGameState()
			};

			_context.GameState.Add(gameState);
			_context.SaveChanges();

			//you're going to configure your client app to listen for this
			Clients.All.SendAsync("Send", supply);
			Clients.All.SendAsync("Game", game.GameId);
			Clients.User(ben.PlayerName).SendAsync("Player", ben);
			Clients.User(maria.PlayerName).SendAsync("Player", maria);

		}

		public async Task LoadGame(int gameId)
		{			
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			
			var game = Game.Game.Load(gameState.State);

			await Clients.All.SendAsync("Send", game.Supply);
			await Clients.All.SendAsync("Game", game.GameId);

			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
		}
		
		public async Task EndTurn(int gameId)
		{	
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, PlayerAction.EndTurn);
			
			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();
 
			await Clients.All.SendAsync("ReceiveChatMessage", $"{Context.UserIdentifier}: Ends their turn.");
			
			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
		}
		
		public async Task EndActionPhase(int gameId)
		{	
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, PlayerAction.EndActionPhase);
			
			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();

			await Clients.All.SendAsync("ReceiveChatMessage", $"{Context.UserIdentifier}: Ends their action phase.");
			
			await Clients.User(Context.UserIdentifier).SendAsync("Player", game.Players.Single(x => x.PlayerName == Context.UserIdentifier));
			
		}

		public async Task ProcessNonCardAction(int gameId, DominionWeb.Game.Player.PlayerAction action)
		{
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, action);

			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();
			
			//await Clients.User(Context.UserIdentifier).SendAsync("Player", game.Players.Single(x => x.PlayerName == Context.UserIdentifier));
			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
			await Clients.All.SendAsync("Send", game.Supply);
		}
				
		public async Task ProcessAction(int gameId, PlayerAction action, Card card)
		{
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, action, card);

			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();
			
			//await Clients.User(Context.UserIdentifier).SendAsync("Player", game.Players.Single(x => x.PlayerName == Context.UserIdentifier));
			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
			await Clients.All.SendAsync("Send", game.Supply);
		}
		
		public async Task ProcessActionResponse(int gameId, ActionRequestType actionRequestType, ActionResponse actionResponse)
		{
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, actionRequestType, actionResponse);

			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();
			
			//await Clients.User(Context.UserIdentifier).SendAsync("Player", game.Players.Single(x => x.PlayerName == Context.UserIdentifier));
			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
			await Clients.All.SendAsync("Send", game.Supply);
		}
		
		public async Task ProcessSelectCardsActionResponse(int gameId, ActionRequestType actionRequestType, IEnumerable<Card> cards)
		{
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, actionRequestType, cards);

			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();
			
			//await Clients.User(Context.UserIdentifier).SendAsync("Player", game.Players.Single(x => x.PlayerName == Context.UserIdentifier));
			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
			await Clients.All.SendAsync("Send", game.Supply);
		}
		
		public async Task ProcessSelectOptionsActionResponse(int gameId, ActionRequestType actionRequestType, IEnumerable<ActionResponse> options)
		{
			var gameState = await _context.GameState.Where(x => x.GameId == gameId).FirstAsync();
			var game = Game.Game.Load(gameState.State);
			
			game.Submit(Context.UserIdentifier, actionRequestType, options);

			gameState.State = game.GetGameState();
			_context.GameState.Update(gameState);
			await _context.SaveChangesAsync();
			
			//await Clients.User(Context.UserIdentifier).SendAsync("Player", game.Players.Single(x => x.PlayerName == Context.UserIdentifier));
			foreach (var player in game.Players)
			{
				await Clients.User(player.PlayerName).SendAsync("Player", player);
			}
			await Clients.All.SendAsync("Send", game.Supply);
		}

//	    private class GameState
//	    {
//	        public Supply Supply { get; set; }
//		    public IEnumerable<Player> Players;
//	    }

		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("ReceiveSystemMessage", $"{Context.UserIdentifier} joined.");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.All.SendAsync("ReceiveSystemMessage", $"{Context.UserIdentifier} left.");
			await base.OnDisconnectedAsync(exception);
		}

		public async Task SendToUser(string user, string message)
		{
			await Clients.User(user).SendAsync("ReceiveDirectMessage", $"{Context.UserIdentifier}: {message}");
		}

		public async Task Send(string message)
		{
			await Clients.All.SendAsync("ReceiveChatMessage", $"{Context.UserIdentifier}: {message}");
		}
	}
}
