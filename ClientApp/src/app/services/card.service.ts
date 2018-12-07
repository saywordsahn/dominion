import { Injectable } from '@angular/core';
import {Card} from "../models/card";
import {DataService} from "./data.service";

@Injectable({
  providedIn: 'root',
})
export class CardService {

  private costs: number[];

  Card: Card;

  constructor(private dataService: DataService) {

    this.dataService.getCardCosts()
      .subscribe(x => this.costsReturned(x));
  }

  private costsReturned(costs: number[]) : void {
    console.log({ costs});
    this.costs = costs;
  }

  getCard(cardId: number) {
    return Card[cardId];
  }

  getCost(card: Card)
  {
    return this.costs[card];
  }

  getImgLoc(card: Card) : string
  {
      return `assets/img/${Card[card]}.jpg`;
  }


}

