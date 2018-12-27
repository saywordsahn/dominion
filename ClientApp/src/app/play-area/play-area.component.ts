import {Component} from '@angular/core';
import {Input} from '@angular/core';
import {Card} from "../models/card";
import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {HubService} from "../services/hub.service";
import {GameService} from "../services/game.service";
import {PlayedCard} from "../models/playedCard";


@Component({
  selector: 'd-play-area',
  templateUrl: './play-area.component.html',
})
export class PlayAreaComponent {

  playStack: PlayedCard[];

  @Input()
  set playedCards(playedCards: PlayedCard[]) {
    if (playedCards != undefined) {
      this.playStack = playedCards;
    }
    console.log('playStack');
    console.log(this.playStack);
  }

  public Card: Card;

  showCardDetail: boolean = false;
  cardDetailLoc: string;

  constructor(
    private dataService: DataService,
    public cardService: CardService,
    private hubService: HubService,
    private gameService: GameService
  ) {
    this.playStack = [];
  }

  rightClick(card) {
    this.cardDetailLoc = this.cardService.getImgLoc(card);
    console.log(this.cardDetailLoc);
    this.showCardDetail = true;
  }

  getKeys(map){
    return Array.from(map.keys());
  }

}
