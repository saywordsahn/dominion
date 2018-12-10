namespace DominionWeb.Game.Cards.Abilities
{
    public interface IAbility
    {
        void Resolve(IPlayer player);
    }
}