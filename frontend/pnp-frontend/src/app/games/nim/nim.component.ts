import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SignalRService } from 'src/app/shared/signal-r.service';
import { GameService } from '../game.service';
import { map } from 'rxjs/operators';
import { NimMove } from './models/nim-move';

@Component({
  selector: 'app-nim',
  templateUrl: './nim.component.html',
  styleUrls: ['./nim.component.scss'],
})
export class NimComponent implements OnInit, OnDestroy {
  private playerCount = 2;
  private subscriptions: Subscription[] = [];
  public playerTurn: number;
  public tokenPool: number;
  public curPlayer = 0;

  constructor(
    private signalRService: SignalRService,
    private gameService: GameService
  ) {
    this.playerTurn = this.gameService.getPlayerTurn();
    this.tokenPool = 16;
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.signalRService.gameMoveReceived
        .pipe(map((moveString: string) => JSON.parse(moveString)))
        .subscribe((move: NimMove) => {
          if (move.player === this.curPlayer) {
            this.tokenPool -= move.amountTaken;
            if (this.tokenPool <= 0) {
              this.gameOver();
            } else {
              this.curPlayer = (this.curPlayer + 1) % this.playerCount;
            }
          }
        })
    );
    this.gameService.gameInitialized();
  }
  gameOver() {
    throw new Error('Method not implemented.');
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
    this.gameService.gameOver();
  }

  sendMove(amount: number) {
    if (this.curPlayer == this.playerTurn) {
      const move: NimMove = { player: this.playerTurn, amountTaken: amount };
      this.gameService.sendMove(move);
    }
  }
}
