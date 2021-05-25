import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { User } from 'src/app/auth/models/user';
import { MessageDTO } from 'src/app/shared/models/message-dto';
import { SignalRService } from 'src/app/shared/signal-r.service';
import { ChatMessage } from '../models/chat-message';
@Component({
  selector: 'app-public-chat',
  templateUrl: './public-chat.component.html',
  styleUrls: ['./public-chat.component.scss'],
})
export class PublicChatComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];
  private curUsername: string;
  public userLoggedIn = false;
  public messages: ChatMessage[] = [
    { username: 'Guest', message: 'Ello' },
    { username: 'God', message: 'Yo mama' },
  ];
  constructor(
    private signalR: SignalRService,
    private authService: AuthService
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
    this.subscriptions.push(
      this.authService.$userLoggedIn.subscribe(
        (bool) => (this.userLoggedIn = bool)
      )
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }

  sendMessage(msg: string) {
    this.signalR.sendGlobalChatMessage(this.curUsername, msg);
  }
}
