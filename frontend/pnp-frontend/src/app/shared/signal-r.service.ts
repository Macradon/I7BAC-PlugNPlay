import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: HubConnection;
  public challengeAccepted = new EventEmitter<any>();
  public challengeFrom = new EventEmitter<any>();
  public challengeHesBeenDeclined = new EventEmitter<any>();
  public friendOnline = new EventEmitter<any>();
  public friendRequest = new EventEmitter<any>();
  public friendRequestAccepted = new EventEmitter<any>();
  public queedForGame = new EventEmitter<any>();
  public gameStart = new EventEmitter<any>();
  public recievedGlobalChatMessage = new EventEmitter<any>();
  public recievedGameChatMessage = new EventEmitter<any>();
  public gameMoveRecieved = new EventEmitter<any>();

  constructor() {}

  public connect = () => {
    this.hubConnection = new HubConnectionBuilder()
      //.withUrl("https://plugnplaybackend.azurewebsites.net/globalHub")
      .withUrl('https://localhost:5001/globalHub')
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Websocket successfully connected');
        this.registerEventEmitters();
      })
      .catch((err) =>
        console.log('Error while starting websocket connection: ' + err)
      );
  };

  registerEventEmitters() {
    this.hubConnection.on("QueuedForGame", (data) => {
      this.queedForGame.emit(data);
    });
    this.hubConnection.on("GameStart", (data) => {
      this.gameStart.emit(data);
    });
    this.hubConnection.on("SendMove", (data) => {
      this.gameMoveRecieved.emit(data);
    });
  }

  public sendMesage(cmd: string, room?: string, payload?: any) {
    this.hubConnection
      .invoke(cmd, room, JSON.stringify(payload))
      .catch((err) => console.error(err));
  }
}
