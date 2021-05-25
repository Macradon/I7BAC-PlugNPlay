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
  private playerTurn = 0;

  constructor(private socket: SignalRService, private router: Router) {}

  public getPlayerTurn() {
    return this.playerTurn;
  }

  public queue(gameId: string) {
    this.socket.sendMessage('QueueUpForGame', gameId);
  }
  public startGame(game: Game, roomId: string, playerTurn: number) {
    this.gameRoomId = roomId;
    this.playerTurn = playerTurn;
    this.$gameActive.next(true);
    this.router.navigate([game.Link]);
  }
  public gameOver() {
    this.$gameActive.next(false);
  }

  public gameInitialized() {
    this.socket.sendMessageToRoom(
      'GameInitializationComplete',
      this.gameRoomId
    );
  }
  public sendMove(move: any) {
    this.socket.sendMessageToRoom(
      'SendMove',
      this.gameRoomId,
      JSON.stringify(move)
    );
  }
}
