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

		//TODO: rename echo hub or move logic to GameHub
		//TODO: refactor game startup as builder or factory
		//TODO: performance add homogenous pile class for most piles
		//you're going to invoke this method from the client app
		public void Echo(ChatMessage message)
		{
			var gameModel = new Models.Game()
			{
				DateTime = DateTime.Now
			};

			_context.Game.Add(gameModel);
			_context.SaveChanges();
			
			var supplyFactory = new DefaultSupplyFactory();
			var supply = supplyFactory.Create();

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

		public async Task ProcessNonCardAction(int gameId, PlayerAction action)
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

    public class ChatMessage
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }

}
