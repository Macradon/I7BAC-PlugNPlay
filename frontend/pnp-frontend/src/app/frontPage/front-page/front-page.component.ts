import { Component, OnInit } from '@angular/core';
import { SignalRService } from 'src/app/shared/signal-r.service';

@Component({
  selector: 'app-front-page',
  templateUrl: './front-page.component.html',
  styleUrls: ['./front-page.component.scss'],
})
export class FrontPageComponent implements OnInit {
  constructor(private signalR: SignalRService) {}

  ngOnInit(): void {}

  public send() {}
  public connect() {
    this.signalR.connect();
  }
}
