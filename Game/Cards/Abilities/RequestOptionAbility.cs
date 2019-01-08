using DominionWeb.Game.Common.Requests;

namespace DominionWeb.Game.Cards.Abilities
{
    public class RequestOptionAbility
    {
        public RequestOptionAbility(RequestOption requestOption, IAbility ability)
        {
            RequestOption = requestOption;
            Ability = ability;
        }

        public RequestOption RequestOption { get; set; }
        public IAbility Ability { get; set; }
        
    }
}