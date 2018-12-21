using System;
using System.Collections.Generic;
using System.Linq;

namespace DominionWeb.Game.Supply
{
    public class Supply : ISupply
    {
        public IEnumerable<Pile> TreasureSupply { get; }
        public IEnumerable<Pile> VictorySupply { get; }
        public IEnumerable<Pile> KingdomSupply { get; }
        public ICollection<Card> Trash { get; private set; }
        
        private IEnumerable<Pile> FullSupply => TreasureSupply.Concat(VictorySupply).Concat(KingdomSupply);
        
        private readonly IDictionary<Card, SupplyType> _supplyTypeMap = new Dictionary<Card, SupplyType>();

        //TODO: figure out why we can't make this a property (json serialization issue)
        public IEnumerable<Card> GetDistinctCards()
        {
            return FullSupply.Where(x => x.Cards.Count > 0).Select(x => x.Cards[0]);
        }
        
        public Supply(
            IEnumerable<Pile> treasureSupply,
            IEnumerable<Pile> victorySupply,
            IEnumerable<Pile> kingdomSupply
        )
        {
            TreasureSupply = treasureSupply;
            VictorySupply = victorySupply;
            KingdomSupply = kingdomSupply;
            Trash = new List<Card>();
            MapSupplyType();
        }

        private void MapSupplyType()
        {
            foreach (var pile in TreasureSupply)
            {
                if (pile.Cards.Count > 0)
                {
                    _supplyTypeMap.Add(pile.Cards[0], SupplyType.Treasure);
                }
            }
            
            foreach (var pile in VictorySupply)
            {
                if (pile.Cards.Count > 0)
                {
                    _supplyTypeMap.Add(pile.Cards[0], SupplyType.Victory);
                }
            }
            
            foreach (var pile in KingdomSupply)
            {
                if (pile.Cards.Count > 0)
                {
                    _supplyTypeMap.Add(pile.Cards[0], SupplyType.Kingdom);
                }
            }
        }

        public bool Contains(Card card)
        {
            var cards = TreasureSupply.Select(x => x.Cards)
                .Concat(VictorySupply.Select(x => x.Cards))
                .Concat(KingdomSupply.Select(x => x.Cards));

            return cards.Select(x => x.FirstOrDefault())
                .Contains(card);
        }

        public void AddToTrash(Card card)
        {
            Trash.Add(card);
        }

        public bool NoProvincesRemain()
        {
            return !VictorySupply.Any(pile => pile.Cards.Count > 0 && pile.Cards[0] == Card.Province);
        }

        public bool ThreeOrMorePilesEmpty()
        {
            return EmptyPileCount() >= 3;
        }

        public int EmptyPileCount()
        {
            return TreasureSupply.Count(pile => pile.Cards.Count == 0)
                + VictorySupply.Count(pile => pile.Cards.Count == 0)
                + KingdomSupply.Count(pile => pile.Cards.Count == 0);
        }

        public Card Take(Card card)
        {
            var supplyType = _supplyTypeMap[card];

            return Take(supplyType, card);   
        }

        public Card Take(SupplyType supplyType, Card card)
        {
            switch (supplyType)
            {
                case SupplyType.Victory:
                    return Take(VictorySupply, card);
                    break;
                case SupplyType.Treasure:
                    return Take(TreasureSupply, card);
                    break;
                case SupplyType.Kingdom:
                    return Take(KingdomSupply, card);
                    break;
                default:
                    throw new InvalidOperationException("There is no supply type mapping for this card.");
            }         
        }

        private static Card Take(IEnumerable<Pile> supply, Card card)
        {
            foreach (var pile in supply)
            {
                if (pile.Cards.Count > 0)
                {
                    if (pile.Cards[0] == card)
                    {
                        pile.Cards.RemoveAt(pile.Cards.Count - 1);
                        return card;
                    }
                }                
            }

            throw new InvalidOperationException("Supply does not contain card.");
        }
    }
}