import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root',
})
export class GameService {
  public gameId: number;
  public gainToHand: boolean;

  constructor() {
    this.gainToHand = false;
  }

}
