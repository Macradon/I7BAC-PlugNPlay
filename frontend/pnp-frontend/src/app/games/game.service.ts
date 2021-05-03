import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { SignalRService } from '../shared/signal-r.service';
import { Game } from './models/game';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  public $gameActive = new BehaviorSubject<boolean>(false);
  private gameRoomId = '';
  private playerturn = 0;

  constructor(private socket: SignalRService, private router: Router) {}

  getPlayerTurn() {
    return this.playerturn;
  }

  queue(gameId: string) {
    this.socket.sendMesage('QueueUpForGame', gameId);
  }
  startGame(game: Game, roomId: string, playerTurn: number) {
    this.gameRoomId = roomId;
    this.playerturn = playerTurn;
    this.$gameActive.next(true);
    this.router.navigate([game.Link]);
  }
  gameOver() {
    this.$gameActive.next(false);
  }

  private gameInitialized() {
    this.socket.sendMesageToRoom('GameInitializationComplete', this.gameRoomId);
  }
  sendMove(move: any) {
    this.socket.sendMesageToRoom(
      'ReceiveMove',
      this.gameRoomId,
      JSON.stringify(move)
    );
  }
}
