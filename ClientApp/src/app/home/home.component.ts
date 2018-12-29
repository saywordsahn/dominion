import { Component } from '@angular/core';
import { DataService } from '../services/data.service';
import {HubService} from "../services/hub.service";
import {UserService} from "../services/user.service";
import {GameService} from "../services/game.service";
import {User} from "../models/user";

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
      private gameService: GameService
      ) {
    }

    ngOnInit() {
      //connect to data service
      this.dataService.getData()
        .subscribe(x => HomeComponent.dataReturned(x));

      this.user = this.userService.getUser();

      this.dataService.login('ben@gmail.com', 'a!1bmtickleP')
        .subscribe(x => this.loginReturn(x));

      // this.dataService.register()
      //   .subscribe(x => console.log(x));
    }

    loginReturn(x) {
      console.log(x);
      this.userService.loginToken = x["token"];
      this.hubService.connect(this.userService.loginToken);
    }

    static dataReturned(data: any) {
        console.log(data);
    }

    send() {
      this.hubService.newGame();
    }

    loadPrevious(gameId: number) {

      this.hubService.loadGame(gameId);
    }


    changeUser() {

      //logout current user by disconnecting from hub and removing token
      this.hubService.disconnect();
      this.userService.loginToken = null;

      if (this.user.userId == 1) {
        this.dataService.login('maria@gmail.com', 'b!1bmtickleP')
          .subscribe(x => this.loginReturn(x));
        this.userService.setUser(2);
      } else {
        this.dataService.login('ben@gmail.com', 'a!1bmtickleP')
          .subscribe(x => this.loginReturn(x));
        this.userService.setUser(1);
      }

      this.user = this.userService.getUser();
    }

}
