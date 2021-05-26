import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { MessageDTO } from './models/message-dto';

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
  public receivedGlobalChatMessage = new EventEmitter<MessageDTO>();
  public receivedGameChatMessage = new EventEmitter<MessageDTO>();
  public gameMoveReceived = new EventEmitter<string>();

  constructor() {}

  public connect = () => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://plug-n-play-backend.herokuapp.com/globalHub')
      //.withUrl('https://localhost:5001/globalHub')
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

  public disconnect() {
    this.hubConnection.stop();
  }

  registerEventEmitters() {
    // Queue & Game
    this.hubConnection.on('QueuedUpForGame', (data) => {
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

    // Chat
    this.hubConnection.on('ReceiveGlobalChatMessage', (data) => {
      this.receivedGlobalChatMessage.emit(JSON.parse(data));
    });
    this.hubConnection.on('ReceiveGameChatMessage', (data) => {
      this.receivedGameChatMessage.emit(JSON.parse(data));
    });

    // Friend list
    this.hubConnection.on('FriendOnline', (data) => {
      this.friendOnline.emit(data);
    });
    this.hubConnection.on('FriendRequest', (data) => {
      this.friendRequest.emit(data);
    });
    this.hubConnection.on('FriendRequestAccepted', (data) => {
      this.friendRequestAccepted.emit(data);
    });
  }

  public sendGlobalChatMessage(user: string, msg: string) {
    this.hubConnection
      .invoke('SendMessage', user, msg, 'GlobalChat')
      .catch((err) => console.error(err));
  }

  public sendInGameChatMessage(user: string, msg: string, roomId: string) {
    this.hubConnection
      .invoke('SendMessage', user, msg, roomId)
      .catch((err) => console.error(err));
  }

  public sendMove(move: string, roomId: string) {
    this.hubConnection
      .invoke('SendMove', move, roomId)
      .catch((err) => console.error(err));
  }

  public joinQueue(gameId: string) {
    this.hubConnection
      .invoke('QueueUpForGame', gameId)
      .catch((err) => console.error(err));
  }

  public sendGameInitializationComplete(roomId: string) {
    this.hubConnection
      .invoke('GameInitializationComplete', roomId)
      .catch((err) => console.error(err));
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
        .invoke(cmd, payload, room)
        .catch((err) => console.error(err));
    else
      this.hubConnection.invoke(cmd, room).catch((err) => console.error(err));
  }
}
