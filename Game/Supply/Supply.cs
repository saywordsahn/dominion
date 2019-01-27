
using System;
using System.Collections.Generic;
using System.Linq;
using DominionWeb.Game.Cards;

namespace DominionWeb.Game.Supply
{
    public class Supply : ISupply
    {
        public IEnumerable<IPile> TreasureSupply { get; }
        public IEnumerable<IPile> VictorySupply { get; }
        public IEnumerable<IPile> KingdomSupply { get; }
        
        public bool IncludeRuins { get; set; } = false;
        public IPile RuinsPile { get; set; }
        
        public ICollection<Card> Trash { get; }
                
        private IEnumerable<IPile> FullSupply => TreasureSupply.Concat(VictorySupply).Concat(KingdomSupply);
        
        private readonly IDictionary<Card, SupplyType> _supplyTypeMap = new Dictionary<Card, SupplyType>();

        //TODO: figure out why we can't make this a property (json serialization issue)
        public IEnumerable<Card> GetGainableCards()
        {
            return FullSupply.Where(x => x.Cards.Count > 0).Select(x => x.Cards[x.Cards.Count - 1]);
        }

        public Supply(
            IEnumerable<IPile> treasureSupply,
            IEnumerable<IPile> victorySupply,
            IEnumerable<IPile> kingdomSupply,
            IPile ruinsPile = null)
        {
            TreasureSupply = treasureSupply;
            VictorySupply = victorySupply;
            KingdomSupply = kingdomSupply;

            if (ruinsPile != null)
            {
                RuinsPile = ruinsPile;
                IncludeRuins = true;
            }
            
            Trash = new List<Card>();
            MapSupplyType();
        }

        private void MapSupplyType()
        {
            
//            TreasureSupply
//                .Where(pile => pile.Cards.Count > 0)
//                .Select(x => x.Cards.Distinct())
//                .Aggregate((x, y) => x.Concat(y))
//                .ToList()
//                .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Treasure));
//            
//            VictorySupply
//                .Where(pile => pile.Cards.Count > 0)
//                .Select(x => x.Cards.Distinct())
//                .Aggregate((x, y) => x.Concat(y))
//                .ToList()
//                .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Victory));
//            
//            KingdomSupply
//                .Where(pile => pile.Cards.Count > 0)
//                .Select(pile => pile.Cards.Distinct())
//                .Aggregate((x, y) => x.Concat(y))
//                .ToList()
//                .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Kingdom));
            
            //TODO: use static list for this?
            //think about pros/cons
            foreach (var pile in TreasureSupply)
            {
                if (pile.Cards.Count > 0)
                {
                    pile.Cards
                        .Distinct().ToList()
                        .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Treasure));
                }
            }
            
            foreach (var pile in VictorySupply)
            {
                if (pile.Cards.Count > 0)
                {
                    pile.Cards
                        .Distinct().ToList()
                        .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Victory));
                }
            }
            
            foreach (var pile in KingdomSupply)
            {
                if (pile.Cards.Count > 0)
                {
                    
                    pile.Cards
                        .Distinct().ToList()
                        .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Kingdom));
                }
            }

            if (IncludeRuins)
            {
                if (RuinsPile.Cards.Count > 0)
                {
                    RuinsPile.Cards
                        .Distinct().ToList()
                        .ForEach(x => _supplyTypeMap.Add(x, SupplyType.Ruins));
                        
                }
            }
        }

        //TODO: needs to be modified
        //TODO: implement ruins pile
        public bool Contains(Card card)
        {
            
            var cards = TreasureSupply.Select(x => x.Cards)
                .Concat(VictorySupply.Select(x => x.Cards))
                .Concat(KingdomSupply.Select(x => x.Cards));
            
            return cards.Select(x => x.FirstOrDefault())
                .Contains(card);
        }

        public bool CardIsVisible(Card card)
        {
//            var cards = TreasureSupply.Select(x => x.Cards.Last())
//                .Concat(VictorySupply.Select(x => x.Cards.Last()))
//                .Concat(KingdomSupply.Select(x => x.Cards.Last()));

            var cards = TreasureSupply.Concat(VictorySupply).Concat(KingdomSupply)
                .Where(pile => pile.Cards.Count > 0)
                .Select(pile => pile.Cards.Last());

            return cards.Contains(card);
        }
        
        public bool Contains(Card card, int numberOfCards)
        {
            var cards = TreasureSupply.Select(x => x.Cards)
                .Concat(VictorySupply.Select(x => x.Cards))
                .Concat(KingdomSupply.Select(x => x.Cards));

            return cards.Select(x => x.FirstOrDefault())
                .Count(x => x == card) >= numberOfCards;
        }

        //TODO: cardpileMap needs to be injectable for black market card mappings
        //TODO: update for ruinsPile
        public void Return(Card card)
        {
            if (CardLists.CardPileMap.ContainsKey(card))
            {
                var pile = FullSupply.Single(x => x.PileCard == CardLists.CardPileMap[card]);
                pile.Cards.Add(card);
            }
            else
            {
                var pile = FullSupply.Single(x => x.PileCard == card);
                pile.Cards.Add(card);
            }
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
            var count = TreasureSupply.Count(pile => pile.Cards.Count == 0)
                        + VictorySupply.Count(pile => pile.Cards.Count == 0)
                        + KingdomSupply.Count(pile => pile.Cards.Count == 0);

            if (RuinsPile != null)
            {
                count += + RuinsPile.Cards.Count == 0 ? 1 : 0;
            }

            return count;
        }

        /// <summary>
        /// Removes and returns a card from the supply.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>Removed card.</returns>
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
                case SupplyType.Treasure:
                    return Take(TreasureSupply, card);
                case SupplyType.Kingdom:
                    return Take(KingdomSupply, card);
                case SupplyType.Ruins:
                    return Take(RuinsPile, card);
                default:
                    throw new InvalidOperationException("There is no supply type mapping for this card.");
            }         
        }

        private static Card Take(IPile pile, Card card)
        {
            if (pile.Cards.Last() == card)
            {
                pile.Cards.RemoveAt(pile.Cards.Count - 1);
                return card;
            }
            
            throw new InvalidOperationException("Cannot take that card.");
        }

        private static Card Take(IEnumerable<IPile> supply, Card card)
        {
            foreach (var pile in supply)
            {
                if (pile.Cards.Count > 0)
                {
                    if (pile.Cards[pile.Cards.Count - 1] == card)
                    {
                        pile.Cards.RemoveAt(pile.Cards.Count - 1);
                        return card;
                    }
                }                
            }

            throw new InvalidOperationException("Cannot take that card.");
        }
        
    }
}