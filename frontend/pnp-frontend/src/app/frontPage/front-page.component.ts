import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from 'src/app/shared/signal-r.service';

@Component({
  selector: 'app-front-page',
  templateUrl: './front-page.component.html',
  styleUrls: ['./front-page.component.scss'],
})
export class FrontPageComponent implements OnInit {
  constructor(private signalR: SignalRService, private router: Router) {}

  ngOnInit(): void {}

  public send() {
    this.router.navigate(['games/queue/nim']);
  }
  public connect() {
    this.signalR.connect();
  }
}
