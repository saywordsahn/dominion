import { Component } from '@angular/core';
import { DataService } from '../services/data.service';
import {HubService} from "../services/hub.service";
import {UserService} from "../services/user.service";
import {GameService} from "../services/game.service";
import {User} from "../models/user";
import {MatDialog} from "@angular/material";
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';

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
      private loginDialog: MatDialog
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


    changeUser() {

      //logout current user by disconnecting from hub and removing token
      this.hubService.disconnect();
      this.userService.loginToken = null;

      if (this.user.userId == '21d52470-9a75-4012-ad6b-da787c348f09') {
        this.dataService.login('maria@gmail.com', 'b!1bmtickleP')
          .subscribe(x => this.loginReturn(x));
        this.userService.setUser('add58dab-0f7a-495e-98c6-13f060b367dc2');
      } else {
        this.dataService.login('ben@gmail.com', 'a!1bmtickleP')
          .subscribe(x => this.loginReturn(x));
        this.userService.setUser('21d52470-9a75-4012-ad6b-da787c348f09');
      }

      this.user = this.userService.getUser();
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
