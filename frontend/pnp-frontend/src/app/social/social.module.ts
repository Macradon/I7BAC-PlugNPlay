import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialComponent } from './social.component';
import { PublicChatComponent } from './public-chat/public-chat.component';
import { GameChatComponent } from './game-chat/game-chat.component';
import { ChatComponent } from './chat/chat.component';

@NgModule({
  declarations: [
    SocialComponent,
    PublicChatComponent,
    GameChatComponent,
    ChatComponent,
  ],
  imports: [CommonModule],
  exports: [SocialComponent],
})
export class SocialModule {}
