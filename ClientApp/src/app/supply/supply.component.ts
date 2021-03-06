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

  includeRuins: boolean;
  //todo: ruins pile should only show top card
  ruinsPile: Pile;

  showCardDetail: boolean = false;
  cardDetailLoc: string;

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
    this.includeRuins = supply.includeRuins;
    this.ruinsPile = supply.ruinsPile;
  }

  buy(pile: Pile): void {
      if (pile.cards.length > 0) {
        this.hubService.submitAction(this.gameService.gameId, PlayerAction.Buy, pile.cards[pile.cards.length - 1]);
      }
  }

  rightClick(pile) {
    if (pile.cards.length > 0) {
      this.cardDetailLoc = this.cardService.getImgLoc(pile.cards[pile.cards.length - 1]);
      console.log(this.cardDetailLoc);
      this.showCardDetail = true;
    }
  }

}
