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
  public queuedForGame = new EventEmitter<string>();
  public queueMatchFound = new EventEmitter<number>();
  public gameStart = new EventEmitter<void>();
  public receivedGlobalChatMessage = new EventEmitter<string>();
  public receivedGameChatMessage = new EventEmitter<string>();
  public gameMoveReceived = new EventEmitter<string>();

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
    this.hubConnection.on('QueuedForGame', (data) => {
      this.queuedForGame.emit(data);
      this.hubConnection.on('QueueMatchFound', (data) => {
        this.queueMatchFound.emit(data);
      });
    });
    this.hubConnection.on('GameStart', () => {
      this.gameStart.emit();
    });
    this.hubConnection.on('ReceiveMove', (data) => {
      this.gameMoveReceived.emit(data);
    });
  }

  public sendMessage(cmd: string, payload?: any) {
    if (payload)
      this.hubConnection
        .invoke(cmd, payload)
        .catch((err) => console.error(err));
    else this.hubConnection.invoke(cmd).catch((err) => console.error(err));
  }

  public sendMessageToRoom(cmd: string, room: string, payload?: any) {
    if (payload)
      this.hubConnection
        .invoke(cmd, room, payload)
        .catch((err) => console.error(err));
    else
      this.hubConnection.invoke(cmd, room).catch((err) => console.error(err));
  }
}
