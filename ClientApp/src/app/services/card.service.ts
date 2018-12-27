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

  getCardName(card: Card) : string {
    return Card[card];
  }

  getCost(card: Card)
  {
    return this.costs[card];
  }

  getImgLoc(card: Card) : string
  {
      return `assets/img/${Card[card]}.jpg`;
  }

  getSmallImgLoc(card: Card) : string
  {
    return `assets/img/treasureSupply/${Card[card]}_Small.jpg`;
  }
}

