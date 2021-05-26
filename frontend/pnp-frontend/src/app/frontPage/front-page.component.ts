import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SignalRService } from 'src/app/shared/signal-r.service';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-front-page',
  templateUrl: './front-page.component.html',
  styleUrls: ['./front-page.component.scss'],
})
export class FrontPageComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];
  public userLoggedIn = false;
  constructor(
    private signalR: SignalRService,
    private router: Router,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.auth.$userLoggedIn.subscribe((bool) => (this.userLoggedIn = bool))
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  public newGame() {
    this.router.navigate(['games/queue/60893b5f3665f82c430c5d35']);
  }
  public connect() {
    this.signalR.connect();
  }

  public redirectToProfile() {}
}
