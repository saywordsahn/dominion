using System.Collections.Generic;
using DominionWeb.Game.Cards.Abilities;
using DominionWeb.Game.Cards.Types;
using DominionWeb.Game.Common.Rules;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Renaissance
{
	public class Experiment : ICard, IAction, IRulesHolder, IOnGainAbilityHolder
	{
		public Card Name { get; } = Card.Experiment;
		public int Cost { get; } = 3;
		public CardType CardType { get; } = CardType.Action;

		public bool CanGainAnother { get; set; }
		public bool IsThroned { get; set; }
		
		public Experiment(bool canGainAnother = true, bool isThroned = false)
		{
			CanGainAnother = canGainAnother;
			IsThroned = isThroned;
		}
		
		public void Resolve(Game game)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable<IRule> GetRules(Game game, IPlayer player)
		{
			var rules = new List<IRule>();

			if (!IsThroned)
			{
				rules.Add(new ReturnToSupply(CardLocation.PlayedCards, Card.Experiment));
			}
			
			rules.Add(new PlusActions(1));
			rules.Add(new PlusCards(2));

			return rules;
		}

		public void ResolveOnGainAbilities(IPlayer player)
		{
			if (CanGainAnother)
			{
				player.RuleStack.Push(new GainCard(new Experiment(false)));
			}
		}
	}
}