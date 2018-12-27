import {Component} from '@angular/core';
import {Input} from '@angular/core';
import {Card} from "../models/card";
import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {HubService} from "../services/hub.service";
import {GameService} from "../services/game.service";
import {PlayedCard} from "../models/playedCard";

class PlayStack
{
  card: Card;
  count: number;

  constructor(card: Card) {
   this.card = card;
   this.count = 1;
  }
}

@Component({
  selector: 'd-play-area',
  templateUrl: './play-area.component.html',
})
export class PlayAreaComponent {

  playStack: PlayStack[];


  @Input()
  set playedCards(playedCards: PlayedCard[]) {

    this.playStack = [];
    if (playedCards != undefined && playedCards.length > 0)
    {
      let current = new PlayStack(playedCards[0].card.name);

      for (let x = 1; x < playedCards.length; x++)
      {
        if (playedCards[x].card.name === current.card) {
          current.count++;
        } else {
          this.playStack.push(current);
          current = new PlayStack(playedCards[x].card.name);
        }
      }
      this.playStack.push(current);
    }
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
