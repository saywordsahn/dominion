using System;
using System.Collections.Generic;
using DominionWeb.Game.Cards.Base;
using DominionWeb.Game.Cards.Hinterlands;
using DominionWeb.Game.Cards.Renaissance;
using DominionWeb.Game.Cards.Seaside;

namespace DominionWeb.Game.Cards
{

    public static class CardFactory
    {
        //could use an array since we have an enum for all cards
        private static readonly Dictionary<Card, Func<ICard>> _dictionary = new Dictionary<Card, Func<ICard>>() {
            { Card.Copper, () => new Copper() },
            { Card.Silver, () => new Silver() },
            { Card.Gold, () => new Gold() },
            { Card.Estate, () => new Estate() },
            { Card.Duchy, () => new Duchy() },
            { Card.Province, () => new Province() },
            { Card.Curse, () => new Curse() },
            { Card.Cellar, () => new Cellar() },
            { Card.Chapel, () => new Chapel() },
            { Card.Moat, () => new Moat() },
            { Card.Harbinger, () => new Harbinger() },
            { Card.Merchant, () => new Merchant() },
            { Card.Vassal, () => new Vassal() },
            { Card.Village, () => new Village() },
            { Card.Workshop, () => new Workshop() },
            { Card.Bureaucrat, () => new Bureaucrat() },
            { Card.Gardens, () => new Gardens() },
            { Card.Militia, () => new Militia() },
            { Card.Moneylender, () => new Moneylender() },
            { Card.Poacher, () => new Poacher() },
            { Card.Remodel, () => new Remodel() },
            { Card.Smithy, () => new Smithy() },
            { Card.ThroneRoom, () => new ThroneRoom() },
            { Card.Bandit, () => new Bandit() },
            { Card.CouncilRoom, () => new CouncilRoom() },
            { Card.Festival, () => new Festival() },
            { Card.Laboratory, () => new Laboratory() },
            { Card.Library, () => new Library() },
            { Card.Market, () => new Market() },
            { Card.Mine, () => new Mine() },
            { Card.Sentry, () => new Sentry() },
            { Card.Witch, () => new Witch() },
            { Card.Artisan, () => new Artisan() },
            
            //seaside
            { Card.Lighthouse, () => new Lighthouse() },
            
            //hinterlands
            { Card.NomadCamp, () => new NomadCamp()},
            
            //renaissance
            { Card.Ducat, () => new Ducat() },
            { Card.Priest, () => new Priest() }
        };

        public static ICard Create(Card card)
        {
            return _dictionary[card]();
        }

        public static int[] GetCardCostArray()
        {
            var cardCostArray = new int[_dictionary.Count];
            
            foreach (var c in _dictionary)
            {
                cardCostArray[(int)c.Key] = Create(c.Key).Cost;
            }

            return cardCostArray;
        }

    }
}