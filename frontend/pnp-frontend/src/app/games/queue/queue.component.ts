import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { SignalRService } from 'src/app/shared/signal-r.service';
import { GameService } from '../game.service';
import { Game } from '../models/game';

@Component({
  selector: 'app-queue',
  templateUrl: './queue.component.html',
  styleUrls: ['./queue.component.scss'],
})
export class QueueComponent implements OnInit, OnDestroy {
  public gameId = '';
  private subscriptions: Subscription[] = [];
  private gameRoomId = '';
  private playerTurn = 0;
  public game: Game = {
    _id: '',
    Name: 'Nim',
    Link: 'games/nim',
    Pic: '',
    Desc: 'Lorem Isum',
    Players: 2,
  };

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private socket: SignalRService
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.route.params.subscribe((params) => {
        this.gameId = params['id'];

        // TODO API call to load the details here.
        this.gameService.queue(this.gameId);
      })
    );
    this.subscriptions.push(
      this.socket.queueMatchFound.subscribe((turnNumber: number) => {
        this.playerTurn = turnNumber;
        this.gameService.startGame(this.game, this.gameRoomId, this.playerTurn);
      })
    );

    this.subscriptions.push(
      this.socket.queedForGame.subscribe(
        (roomId: string) => (this.gameRoomId = roomId)
      )
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }
}
