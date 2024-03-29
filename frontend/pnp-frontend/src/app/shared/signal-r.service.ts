import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: HubConnection;

  constructor() {}

  public connect = () => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("https://plugnplaybackend.azurewebsites.net/globalHub")
      //.withUrl('https://localhost:5001/globalHub')
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('Game connection started');
        this.registerEventEmitters();
      })
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  registerEventEmitters() {}
}
