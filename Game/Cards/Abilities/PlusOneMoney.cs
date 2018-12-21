namespace DominionWeb.Game.Cards.Abilities
{
    public class PlusOneMoney : IAbility
    {
        public bool Resolved { get; set; }

        public void Resolve(IPlayer player)
        {
            player.MoneyPlayed++;
            Resolved = true;
        }

    }
}