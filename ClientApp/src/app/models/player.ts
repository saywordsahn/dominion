import {Card, ICard} from "./card";
import {PlayStatus} from './playStatus';
import {PlayArea} from "./playArea";
import {ActionRequest} from "./actionRequest";
import {PlayedCard} from "./playedCard";

export class Player {
  playerId: number;
  deck: Card[];
  hand: Card[];
  discardPile: Card[];
  playedCards: PlayedCard[];
  playStatus: PlayStatus;
  moneyPlayed: number;
  numberOfBuys: number;
  numberOfActions: number;
  actionRequest: any;
  gameLog: string[];
  victoryPoints: number;
  coffers: number;
  villagers: number;
}
