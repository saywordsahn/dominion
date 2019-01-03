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
import {Artifact, IArtifact} from "../models/artifact";

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

  coffers: number;
  villagers: number;
  artifacts: IArtifact[];
  displayArtifacts: string;

  // private selectMultipleCardsActionRequest: SelectMultipleCardsActionRequest;


  //temp vars for testing multiselect
  selectCards: SelectItem[];
  selectedCards: any[];

  selectOptions: SelectItem[];
  selectedOptions: any[];

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
    this.artifacts = [];
  }

  public Card = Card;

  ngOnInit() {
    this.dataService.playerSubject.subscribe(x => this.updatePlayer(x));
  }

  updatePlayer(player: Player) : void {
    console.log('updatePlayer', player);
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

    this.coffers = player.coffers;
    this.villagers = player.villagers;


    this.artifacts = player.artifacts;
    this.displayArtifacts = '';
    let a = [];
    player.artifacts.forEach((x) => {
      a.push(Artifact[x.artifact]);
    });
    this.displayArtifacts = a.join(', ');

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
    }


    this.selectOptions = [];
    this.selectedOptions = [];
    if (this.actionRequest != undefined && this.actionRequest.actionRequestType == ActionRequestType.SelectOptions) {
      for (let i = 0; i < this.actionRequest.options.length; i++)
      {
        let option = {label: this.actionRequest.options[i].actionResponseText, value: {id: i, name: this.actionRequest.options[i].actionResponse}};
        this.selectOptions.push(option);
        console.log(option);
        console.log(this.selectOptions);
      }
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

  //TODO: Remove PlayAllTreasures dialog when no treasures are present
  playAllTreasure() {
    this.hubService.submitNonCardAction(this.gameService.gameId, PlayerAction.PlayAllTreasure);
  }

  playCoffer() {
    this.hubService.submitNonCardAction(this.gameService.gameId, PlayerAction.PlayCoffer);
  }

  playAllCoffers() {
    this.hubService.submitNonCardAction(this.gameService.gameId, PlayerAction.PlayAllCoffers);
  }

  playVillager() {
    this.hubService.submitNonCardAction(this.gameService.gameId, PlayerAction.PlayVillager);
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

  submitSelectOptions() {
    console.log('selectedOptions');
    console.log(this.selectedOptions);
    //unadapt from primeNg format
    let options = [];
    for (let i = 0; i < this.selectedOptions.length; i++)
    {
      options.push(this.selectedOptions[i].name);
    }
    this.hubService.submitSelectOptionsRequestResponse(this.gameService.gameId, ActionRequestType.SelectOptions, options);
  }

}
