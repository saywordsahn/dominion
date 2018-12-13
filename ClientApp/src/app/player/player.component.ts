import {Component} from '@angular/core';
import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {Card, ICard} from "../models/card";
import {Player} from "../models/player";
import {HubService} from "../services/hub.service";
import {PlayStatus} from "../models/playStatus";
import {GameService} from "../services/game.service";
import {PlayerAction} from '../models/playerAction';
import {ActionRequest} from "../models/actionRequest";
import {ActionRequestType} from "../models/actionRequestType";
import {ActionResponse} from "../models/actionResponse";
import {SelectItem} from "primeng/api";
import {PlayedCard} from "../models/playedCard";

//
// class SelectMultipleCardsActionRequest {
//   message: string;
//   cards: Card[];
// }

@Component({
  selector: 'd-player',
  templateUrl: './player.component.html',
})
export class PlayerComponent implements Player {
  playerId: number;
  deck: Card[];
  discardPile: Card[];
  hand: Card[];
  playStatus: PlayStatus;
  moneyPlayed: number = 0;
  numberOfActions: number;
  numberOfBuys: number;
  playedCards: PlayedCard[];
  actionRequest: any;
  gameLog: string[];
  victoryPoints: number;

  // private selectMultipleCardsActionRequest: SelectMultipleCardsActionRequest;


  //temp vars for testing multiselect
  selectCards: SelectItem[];
  selectedCards: any[];

  logText: string;
  PlayStatus = PlayStatus;
  ActionRequestType = ActionRequestType;

  constructor(
    private dataService: DataService,
    public cardService: CardService,
    private hubService: HubService,
    private gameService: GameService
  ) {
    this.deck = [];
    this.discardPile = [];
    this.hand = [];
    this.playedCards = [];
    this.gameLog = [];
  }

  public Card = Card;

  ngOnInit() {
    this.dataService.playerSubject.subscribe(x => this.updatePlayer(x));
  }

  updatePlayer(player: Player) : void {
    this.deck = player.deck;
    this.discardPile = player.discardPile;
    this.hand = player.hand;
    this.playerId = player.playerId;
    this.playStatus = player.playStatus;
    this.moneyPlayed = player.moneyPlayed;
    this.numberOfActions = player.numberOfActions;
    this.numberOfBuys = player.numberOfBuys;
    this.actionRequest = player.actionRequest;
    this.logText = player.gameLog.join('\n');
    this.victoryPoints = player.victoryPoints;

    // if (player.actionRequest.actionRequestType == ActionRequestType.SelectMultipleCards) {
    //   this.selectMultipleCardsActionRequest = new SelectMultipleCardsActionRequest();
    //   this.selectMultipleCardsActionRequest.message = player.actionRequest.message;
    //   this.selectMultipleCardsActionRequest.cards = player.actionRequest.cards;
    // }

    //todo: add lodash or pipe
    //hide throned cards
    this.playedCards = [];
    for (let i = 0; i < player.playedCards.length; i++) {
      if (!player.playedCards[i].isThronedCopy) {
        this.playedCards.push(player.playedCards[i]);
      }
    }

    this.selectCards = [];
    this.selectedCards = [];

    if (this.actionRequest != undefined && this.actionRequest.actionRequestType == ActionRequestType.SelectMultipleCards) {
      for (let i = 0; i < this.actionRequest.cards.length; i++)
      {
        this.selectCards.push( {label: Card[this.actionRequest.cards[i]], value: {id: i, name: this.actionRequest.cards[i]}});
      }
      console.log('player updated:', this);
    }

  }

  play(card: Card) : void {
    // if (this.playStatus == PlayStatus.ActionPhase)
    // {
    //
    // }
    this.hubService.submitAction(this.gameService.gameId, PlayerAction.Play, card);
  }

  endTurn() {
    this.hubService.endTurn(this.gameService.gameId);
  }

  endActionPhase() {
    this.hubService.endActionPhase(this.gameService.gameId);
  }

  takeAttackEffect() {
   this.hubService.submitNonCardAction(this.gameService.gameId, PlayerAction.TakeAttackEffect);
  }

  playMoat() {
    console.log('not enabled atm');
  }

  //TODO: Remove PlayAllTreasures dialog when no treasures are present
  playAllTreasure() {
    this.hubService.submitNonCardAction(this.gameService.gameId, PlayerAction.PlayAllTreasure);
  }

  submitAction(obj) {
    if (obj == true) {
      this.hubService.submitActionRequestResponse(this.gameService.gameId, ActionRequestType.YesNo, ActionResponse.Yes);
    } else {
      this.hubService.submitActionRequestResponse(this.gameService.gameId, ActionRequestType.YesNo, ActionResponse.No);
    }
  }

  submitMultiSelect() {
    console.log(this.selectedCards);
    //unadapt from primeNg format
    let cards = [];
    for (let i = 0; i < this.selectedCards.length; i++)
    {
      cards.push(this.selectedCards[i].name);
    }
    this.hubService.submitSelectCardsRequestResponse(this.gameService.gameId, ActionRequestType.SelectMultipleCards, cards);
  }

}
