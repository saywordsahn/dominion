import { Component } from '@angular/core';
import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {Card, ICard} from "../models/card";
import {Player} from "../models/player";
import {HubService} from "../services/hub.service";
import {PlayStatus} from "../models/playStatus";
import {GameService} from "../services/game.service";
import {PlayerAction} from '../models/playerAction';
import {PlayArea} from "../models/playArea";
import {ActionRequest} from "../models/actionRequest";
import {ActionRequestType} from "../models/actionRequestType";
import {ActionResponse} from "../models/actionResponse";
import {SelectItem} from "primeng/api";
import {forEach} from "@angular/router/src/utils/collection";

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
  playedCards: ICard[];
  actionRequest: ActionRequest;
  gameLog: string[];


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
    this.playedCards = player.playedCards;
    this.moneyPlayed = player.moneyPlayed;
    this.numberOfActions = player.numberOfActions;
    this.numberOfBuys = player.numberOfBuys;
    this.actionRequest = player.actionRequest;
    this.logText = player.gameLog.join('\n');

    //adapting code for ngPrime controls
    this.selectCards = [];
    this.selectedCards = [];
    for (let i = 0; i < player.hand.length; i++)
    {
      this.selectCards.push( {label: Card[player.hand[i]], value: {id: i, name: player.hand[i]}});
    }
    console.log('player updated:', this);
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
