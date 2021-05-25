import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { GameService } from '../games/game.service';

@Component({
  selector: 'app-social',
  templateUrl: './social.component.html',
  styleUrls: ['./social.component.scss'],
})
export class SocialComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];
  public gameActive = false;

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.gameService.$gameActive.subscribe((bool) => (this.gameActive = bool))
    );
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }
}
