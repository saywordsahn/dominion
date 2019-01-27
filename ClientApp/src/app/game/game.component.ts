import { Component } from '@angular/core';
import { DataService } from '../services/data.service';
import {HubService} from "../services/hub.service";
import {UserService} from "../services/user.service";
import {GameService} from "../services/game.service";
import {User} from "../models/user";
import {MatDialog} from "@angular/material";
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { GameLogService } from '../services/game-log.service';

@Component({
  selector: 'd-game',
  templateUrl: './game.component.html',
})
export class GameComponent {

    public user: User;
    public gameToLoad: number;
    public gainToHand: boolean;

    constructor(
      private dataService: DataService,
      private hubService: HubService,
      private userService: UserService,
      private gameService: GameService,
      private loginDialog: MatDialog,
      public gameLogService: GameLogService
      ) {
    }

    ngOnInit() {
      //connect to data service
      //this.dataService.getData()
       //.subscribe(x => GameComponent.dataReturned(x));

      this.user = this.userService.getUser();

      // this.dataService.register()
      //   .subscribe(x => console.log(x));
    }

    loginReturn(x) {
      console.log(x);
        this.userService.loginToken = x["token"];
        this.userService.userName = x["userName"];
      this.hubService.connect(this.userService.loginToken);
    }

    static dataReturned(data: any) {
        console.log(data);
    }

    newGame(randomizeKingdom: boolean) {
      this.hubService.newGame(randomizeKingdom);
    }

    loadPrevious(gameId: number) {

      this.hubService.loadGame(gameId);
    }
   
    login() {
      const dialogRef = this.loginDialog.open(LoginDialogComponent, {
        width: '250px',
        data: {}
      });

        dialogRef.afterClosed().subscribe(result => {
            console.log(result);
          this.dataService.login(result.userName, result.password)
          .subscribe(x => this.loginReturn(x));
      });
      
    }

}
