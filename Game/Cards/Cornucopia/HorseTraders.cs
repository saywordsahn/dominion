using System.Collections.Generic;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Cornucopia
{
	public class HorseTraders : ICard, IAction, IRulesHolder
	{
		public Card Name { get; } = Card.HorseTraders;
		public int Cost { get; } = 4;
		public CardType CardType { get; } = CardType.Action;


		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			throw new System.NotImplementedException();
		}
	}
}