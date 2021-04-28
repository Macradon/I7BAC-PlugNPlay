import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GamesRoutingModule } from './games-routing.module';
import { QueueComponent } from './queue/queue.component';
import { GameSelectComponent } from './game-select/game-select.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [QueueComponent, GameSelectComponent],
  imports: [CommonModule, GamesRoutingModule, SharedModule],
})
export class GamesModule {}
