using DominionWeb.Game.Cards.Filters;
using DominionWeb.Game.Player;

namespace DominionWeb.Game.Cards.Abilities.Conditions
{
    public class RevealTopCardCondition : IAbilityCondition
    {
        public ICardFilter Filter { get; set; }
        
        public RevealTopCardCondition(ICardFilter filter)
        {
            Filter = filter;
        }

        public bool IsMet(Game game, IPlayer player)
        {
            var topCard = player.TopCard();
            
            player.GameLog.Add(player.PlayerName.Substring(0,1) + " reveals a " + topCard);

            var instance = CardFactory.Create(topCard);

            return Filter.Apply(instance);
        }
    }
}