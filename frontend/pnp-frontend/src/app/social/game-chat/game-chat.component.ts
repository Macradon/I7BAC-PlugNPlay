import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/app/auth/models/user';
import { GameService } from 'src/app/games/game.service';
import { MessageDTO } from 'src/app/shared/models/message-dto';
import { SignalRService } from 'src/app/shared/signal-r.service';
import { ChatMessage } from '../models/chat-message';

@Component({
  selector: 'app-game-chat',
  templateUrl: './game-chat.component.html',
  styleUrls: ['./game-chat.component.scss'],
})
export class GameChatComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];
  private roomId: string = '';
  private curUsername: string;
  public messages: ChatMessage[] = [
    { username: 'Guest', message: 'Ello' },
    { username: 'God', message: 'Yo mama' },
  ];
  constructor(
    private signalR: SignalRService,
    private authService: AuthService,
    private gameService: GameService
  ) {}

  ngOnInit(): void {
    this.subscriptions.push(
      this.signalR.receivedGlobalChatMessage.subscribe((data: MessageDTO) => {
        this.messages.push({ username: data.Sender, message: data.Message });
      })
    );
    this.subscriptions.push(
      this.authService.$currentUser.subscribe(
        (user: User) => (this.curUsername = user.username)
      )
    );
    this.roomId = this.gameService.getRoomId();
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  sendMessage(msg: string) {
    this.signalR.sendInGameChatMessage(this.curUsername, msg, this.roomId);
  }
}
