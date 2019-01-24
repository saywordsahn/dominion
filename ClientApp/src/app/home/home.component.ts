import { Component } from '@angular/core';
import { DataService } from '../services/data.service';
import {HubService} from "../services/hub.service";
import {UserService} from "../services/user.service";
import {GameService} from "../services/game.service";
import {User} from "../models/user";
import {MatDialog} from "@angular/material";
import { LoginDialogComponent } from '../login-dialog/login-dialog.component';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

    public user: User;
    public gameToLoad: number;
    public gainToHand: boolean;

    constructor(
      private dataService: DataService,
      private hubService: HubService,
      private userService: UserService,
      private gameService: GameService,
      private loginDialog: MatDialog,
      private router: Router
      ) {
    }

    ngOnInit() {

      this.user = this.userService.getUser();
      
      // this.dataService.register()
      //   .subscribe(x => console.log(x));
    }

    loginReturn(x) {
      console.log(x);
        this.userService.loginToken = x["token"];
        this.userService.userName = x["userName"];
        this.userService.user = new User(x["userId"], x["userName"]);
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
            if (result.userName == 'b') {
                this.dataService.login('ben@gmail.com', 'a!1bmtickleP')
                  .subscribe(x => this.loginReturn(x));
            }
            else if (result.userName === 'm') {
                this.dataService.login('maria@gmail.com', 'b!1bmtickleP')
                  .subscribe(x => this.loginReturn(x));
            }
            else {
                this.dataService.login(result.userName, result.password)
                  .subscribe(x => this.loginReturn(x));
            }
      });
    }

    goToLobby() {
      this.router.navigate(['/lobby']);
    }

}
