import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SocialComponent } from './social.component';
import { PublicChatComponent } from './public-chat/public-chat.component';
import { GameChatComponent } from './game-chat/game-chat.component';
import { ChatComponent } from './chat/chat.component';
import { SharedModule } from '../shared/shared.module';
import { FriedListComponent } from './fried-list/fried-list.component';
import { ListElementComponent } from './fried-list/list-element/list-element.component';
import { RequestComponent } from './fried-list/request/request.component';

@NgModule({
  declarations: [
    SocialComponent,
    PublicChatComponent,
    GameChatComponent,
    ChatComponent,
    FriedListComponent,
    ListElementComponent,
    RequestComponent,
  ],
  imports: [CommonModule, SharedModule],
  exports: [SocialComponent],
})
export class SocialModule {}
