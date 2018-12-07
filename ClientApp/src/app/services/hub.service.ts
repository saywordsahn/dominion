import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import {DataService} from "./data.service";
import {GameService} from "./game.service";
import {Card} from '../models/card';
import {PlayerAction} from '../models/playerAction';
import {Player} from "../models/player";
import {ActionRequestType} from "../models/actionRequestType";
import {ActionResponse} from "../models/actionResponse";


@Injectable({
  providedIn: 'root',
})
export class HubService {
  public hubConnection: HubConnection;

  constructor(private dataService: DataService,
    private gameService: GameService
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

    // this.hubConnection.on("Update", (message) => {
    //   this.messages.push("MY GAME MESSAGE");
    // });

    // starting the connection
    this.hubConnection.start();
  }

  async disconnect() {
    if (this.hubConnection) {
      await this.hubConnection.stop();
      this.hubConnection = null;
    }
  }



  newGame() {
    this.hubConnection.invoke("Echo", { UserName: "Benj", Message: 'start new game' });
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
    this.hubConnection.send("EndTurn", gameId);
  }

  endActionPhase(gameId: number): void {
    this.hubConnection.send("EndActionPhase", gameId);
  }

  submitActionRequestResponse(gameId: number, actionRequestType: ActionRequestType, actionResponse: ActionResponse) {
    this.hubConnection.send("ProcessActionResponse", gameId, actionRequestType, actionResponse);
  }

}
