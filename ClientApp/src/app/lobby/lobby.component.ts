import { Component } from '@angular/core';
import { Supply } from '../models/supply';

import {DataService} from "../services/data.service";
import {CardService} from "../services/card.service";
import {Pile} from "../models/pile";
import { Card } from '../models/card';
import {HubService} from "../services/hub.service";
import {GameService} from "../services/game.service";
import {PlayerAction} from "../models/playerAction";

import {User} from '../models/user';
import { UserService } from '../services/user.service';
import { Lobby } from '../models/lobby';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';


@Component({
  selector: 'd-lobby',
  templateUrl: './lobby.component.html',
})
export class LobbyComponent {
    currentLobby: Lobby;
    gameList: Lobby[];

  constructor(private dataService: DataService,
              public cardService: CardService,
              private hubService: HubService,
              private gameService: GameService,
              private userService: UserService,
              private router: Router) { 
    this.gameList = [];
  }
              
  ngOnInit() {
      this.dataService.LobbySubject.subscribe(x => this.updateLobbies(x));
      this.dataService.getLobbies().subscribe(x => this.updateLobbies(x));
  }
  
    updateLobbies(lobbies: Lobby[]): void {
        console.log('lobbyComponent.updateLobbies');
        console.log(lobbies);
        this.gameList = lobbies;

        var game = this.gameList.find(x => x.lobbyUser.find(y => y.userId == this.userService.user.userId) != undefined);

        if (game != undefined) {
            this.currentLobby = game;
        }
    }

  createLobby() {
      //this.gameList.push(new Lobby(this.userService.user.userName + "'s game", this.userService.user));
      this.hubService.createLobby(this.userService.user.userName + "'s game");
  }

    join(lobby: Lobby) : void {
        console.log(this.userService.user);
        //lobby.users.push(this.userService.user);
        this.currentLobby = lobby;
        this.hubService.joinLobby(lobby.lobbyId);
    }

    view(lobby: Lobby): void {
        this.currentLobby = lobby;
    }

    leaveCurrentLobby() {
        this.hubService.leaveLobby(this.currentLobby.lobbyId);
        this.currentLobby = undefined;
    }

    startGame(lobbyId: number) {
        this.hubService.startGame(lobbyId);
    }
}
