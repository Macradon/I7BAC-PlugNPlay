import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SignalRService } from 'src/app/shared/signal-r.service';
import { GameService } from '../game.service';
import { NimMove } from './models/nim-move';

@Component({
  selector: 'app-nim',
  templateUrl: './nim.component.html',
  styleUrls: ['./nim.component.scss'],
})
export class NimComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];
  public playerTurn: number;

  constructor(
    private signalRService: SignalRService,
    private gameService: GameService
  ) {
    this.playerTurn = this.gameService.getPlayerTurn();
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.signalRService.gameMoveReceived.subscribe((moveString: string) => {
        const move: NimMove = JSON.parse(moveString);
        console.log('MOVE_RECEIVED: ', move);
      })
    );
    this.gameService.gameInitialized();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  sendMove(amount: number) {
    const move: NimMove = { player: this.playerTurn, amountTaken: amount };
    this.gameService.sendMove(move);
  }
}
