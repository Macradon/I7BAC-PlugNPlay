import { OnDestroy, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { SignalRService } from './shared/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(private websocket: SignalRService) {}

  ngOnInit(): void {
    this.websocket.connect();
  }

  ngOnDestroy(): void {
    this.websocket.disconnect();
  }
}
