import { Component } from '@angular/core';
import { Supply } from '../models/supply';

import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {Pile} from "../models/pile";
import { Card } from '../models/card';
import {HubService} from "../services/hub.service";
import {GameService} from "../services/game.service";
import {PlayerAction} from "../models/playerAction";

@Component({
  selector: 'd-supply',
  templateUrl: './supply.component.html',
})
export class SupplyComponent implements Supply {
  treasureSupply: Pile[];
  kingdomSupply: Pile[];
  victorySupply: Pile[];

  constructor(private dataService: DataService,
              public cardService: CardService,
              private hubService: HubService,
              private gameService: GameService) { }

  public Card = Card;

  ngOnInit() {
    this.dataService.supplySubject.subscribe(x => this.updateSupply(x));
  }

  private updateSupply(supply: Supply) : void {
    console.log(supply);
    this.treasureSupply = supply.treasureSupply;
    this.victorySupply = supply.victorySupply;
    this.kingdomSupply = supply.kingdomSupply;
  }

  buy(pile: Pile): void {
    if (this.gameService.gainToHand) {
      this.hubService.submitAction(this.gameService.gameId, PlayerAction.GainToHand, pile.cards[0])
    } else {
      if (pile.cards.length > 0) {
        this.hubService.submitAction(this.gameService.gameId, PlayerAction.Buy, pile.cards[0]);
      }
    }
  }

}
