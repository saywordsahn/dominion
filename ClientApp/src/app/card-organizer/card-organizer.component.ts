import {Component, Inject} from '@angular/core';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';

import {Input} from '@angular/core';
import {Card} from "../models/card";
import {PlayerAction} from "../models/playerAction";
import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {HubService} from "../services/hub.service";
import {GameService} from "../services/game.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";

export interface CardOrganizerData
{
  cards: Card[];
}

@Component({
  selector: 'd-card-organizer',
  templateUrl: './card-organizer.component.html',
  styleUrls: ['./card-organizer.component.css']
})
export class CardOrganizerComponent {

  public Card: Card;

  drop(event: CdkDragDrop<Card[]>) {
    moveItemInArray(this.data.cards, event.previousIndex, event.currentIndex);
  }

  constructor(public dialogRef: MatDialogRef<CardOrganizerComponent>,
              @Inject(MAT_DIALOG_DATA) public data: CardOrganizerData,
              public cardService: CardService) {
  }

  //
  // organizedHand: Map<Card, number>;
  //
  // @Input()
  // set hand(hand: Card[]) {
  //   this.organizedHand = new Map();
  //   hand.forEach(card => {
  //     if (this.organizedHand.has(card)) {
  //       this.organizedHand.set(card, this.organizedHand.get(card) + 1);
  //     } else {
  //       this.organizedHand.set(card, 1);
  //     }
  //   });
  // }
  //
  // public Card: Card;
  //
  // showCardDetail: boolean = false;
  // cardDetailLoc: string;
  //
  // constructor(
  //   private dataService: DataService,
  //   public cardService: CardService,
  //   private hubService: HubService,
  //   private gameService: GameService
  // ) {
  //   this.hand = [];
  //   this.organizedHand = new Map();
  // }
  //
  // public play(card: Card) : void {
  //   this.hubService.submitAction(this.gameService.gameId, PlayerAction.Play, card);
  // }
  //
  // rightClick(card) {
  //   this.cardDetailLoc = this.cardService.getImgLoc(card);
  //   console.log(this.cardDetailLoc);
  //   this.showCardDetail = true;
  // }
  //
  // getKeys(map){
  //   return Array.from(map.keys());
  // }

}
