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

  public newGame() {
    this.router.navigate(['games/queue/60893b5f3665f82c430c5d35']);
  }
  public connect() {
    this.signalR.connect();
  }

  public redirectToProfile() {}
}
