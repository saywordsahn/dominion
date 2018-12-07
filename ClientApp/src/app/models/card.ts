import {CardType} from "./cardType";

export enum Card {
  Copper,
  Silver,
  Gold,
  Estate,
  Duchy,
  Province,
  Curse,
  Cellar,
  Chapel,
  Moat,
  Harbinger,
  Merchant,
  Vassal,
  Village,
  Workshop,
  Bureaucrat,
  Gardens,
  Militia,
  Moneylender,
  Poacher,
  Remodel,
  Smithy,
  ThroneRoom,
  Bandit,
  CouncilRoom,
  Festival,
  Laboratory,
  Library,
  Market,
  Mine,
  Sentry,
  Witch,
  Artisan
}

export interface ICard {
  name: string;
  cost: number;
  cardType: CardType
}

//
// export enum Card {
//   Copper = "Copper",
//   Silver = "Silver",
//   Gold = "Gold",
//   Estate = "Estate",
//   Duchy = "Duchy",
//   Province = "Province"
// }
