import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import {DataService} from "./data.service";
import {GameService} from "./game.service";
import {Card} from '../models/card';
import {PlayerAction} from '../models/playerAction';
import {Player} from "../models/player";
import {ActionRequestType} from "../models/actionRequestType";
import {ActionResponse} from "../models/actionResponse";
import {Action} from "rxjs/internal/scheduler/Action";
import {Observable, Subject} from 'rxjs';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';



@Injectable({
  providedIn: 'root',
})
export class HubService {
  public hubConnection: HubConnection;

    public connected : Subject<boolean> = new Subject();

  constructor(private dataService: DataService,
      private gameService: GameService,
            private router: Router
  ) { }

  public connect(accessToken: string): void {
    let builder = new HubConnectionBuilder();


    console.log('connecting to hub with token', accessToken);
    // as per setup in the startup.cs
    this.hubConnection = builder.withUrl('/hubs/echo', { accessTokenFactory: () => accessToken }).build();

    // message coming from the server
    this.hubConnection.on("Send", (message) => {
      this.dataService.supplySubject.next(message);
    });

    this.hubConnection.on("Player", (message) => {
      this.dataService.playerSubject.next(message);
    });

    this.hubConnection.on("Game", (gameId) => {
      this.gameService.gameId = gameId;
    });

    this.hubConnection.on("Action", (message) => {
      console.log("action message");
      console.log(message);
    });

    this.hubConnection.on("ReceiveSystemMessage", (message) => {
      console.log("system message");
      console.log(message);
    });

    this.hubConnection.on("ReceiveDirectMessage", (message) => {
      console.log("direct message");
      console.log(message);
    });

    this.hubConnection.on("ReceiveChatMessage", (message) => {
      console.log("chat message");
      console.log(message);
    });

    this.hubConnection.on("LobbyCreated", (lobby) => {
       lobby.users = [];
      this.dataService.LobbySubject.next(lobby);
    });

      this.hubConnection.on("LobbiesUpdated", (lobbies) => {
          this.dataService.LobbySubject.next(lobbies);
      })

      this.hubConnection.on("JoinLobby", (user, lobby) => {
          console.log('hubService', user, lobby);
        this.dataService.LobbySubject.next(lobby);
      });

      this.hubConnection.on("GoToGame", (gameId) => {
        this.router.navigate(['/game']);
    });
    

    // this.hubConnection.on("Update", (message) => {
    //   this.messages.push("MY GAME MESSAGE");
    // });

    // starting the connection
      this.hubConnection.start();
          //.then(this.connected.next(true))
         //.then(this.hubConnection.invoke("GetLobbies"));
  }

  async disconnect() {
    if (this.hubConnection) {
      await this.hubConnection.stop();
      this.hubConnection = null;
    }
  }

    getLobbies(): void {
        this.hubConnection.invoke("GetLobbies");
    }
  createLobby(name: string) {
    this.hubConnection.invoke("CreateLobby", name);
  }

  joinLobby(lobbyId: number) {
     this.hubConnection.invoke("JoinLobby", lobbyId);
  }

  leaveLobby(lobbyId: number) {
      this.hubConnection.invoke("LeaveLobby", lobbyId);
  }

   startGame(lobbyId: number) : void {
    this.hubConnection.invoke("StartGame", lobbyId);
   }

  newGame(randomizedKingdom: boolean) {
    this.hubConnection.invoke("NewGame", randomizedKingdom);
  }

  submitAction(gameId: number, action: PlayerAction, card: Card) {
    console.log({ gameId, action, card} );
    this.hubConnection.send("ProcessAction", gameId, action, card);
  }

  submitNonCardAction(gameId: number, action: PlayerAction) {
    console.log( {gameId, action });
    this.hubConnection.send("ProcessNonCardAction", gameId, action);
  }

  sendSystemMessage(message) {
    this.hubConnection.invoke("Send", message);
  }

  sendPrivateMessage(user: string, message: string) {
    this.hubConnection.send("SendToUser", "maria@gmail.com", message);
  }

  loadGame(gameId: number) : void {
    this.hubConnection.send("LoadGame", gameId);
  }

  endTurn(gameId: number): void {
    console.log('end turn', gameId);
    this.hubConnection.send("EndTurn", gameId);
  }

  endActionPhase(gameId: number): void {
    this.hubConnection.send("EndActionPhase", gameId);
  }

  //TODO: refactor
  submitActionRequestResponse(gameId: number, actionRequestType: ActionRequestType, actionResponse: ActionResponse) {
    this.hubConnection.send("ProcessActionResponse", gameId, actionRequestType, actionResponse);
  }

  submitSelectCardsRequestResponse(gameId: number, actionRequestType: ActionRequestType, cards: Card[])
  {
    this.hubConnection.send("ProcessSelectCardsActionResponse", gameId, actionRequestType, cards);
  }

  submitSelectOptionsRequestResponse(gameId: number, actionRequestType: ActionRequestType, options: ActionResponse[])
  {
    console.log('hub');
    console.log({gameId, actionRequestType, options});
    this.hubConnection.send("ProcessSelectOptionsActionResponse", gameId, actionRequestType, options);
  }

}
