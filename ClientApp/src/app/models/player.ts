import {Card, ICard} from "./card";
import {PlayStatus} from './playStatus';
import {PlayArea} from "./playArea";
import {ActionRequest} from "./actionRequest";

export class Player {
  playerId: number;
  deck: Card[];
  hand: Card[];
  discardPile: Card[];
  playedCards: ICard[];
  playStatus: PlayStatus;
  //playArea: PlayArea;
  moneyPlayed: number;
  numberOfBuys: number;
  numberOfActions: number;
  actionRequest: ActionRequest;
  gameLog: string[];
}
