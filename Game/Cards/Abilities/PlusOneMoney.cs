namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusOneMoney : IAbility
    {
        public void Resolve(IPlayer player)
        {
            player.MoneyPlayed++;
        }
    }
}