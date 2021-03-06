import {Pile} from "./pile";

export class Supply {
  treasureSupply: Pile[];
  victorySupply: Pile[];
  kingdomSupply: Pile[];
  includeRuins: boolean;
  ruinsPile: Pile;
}
