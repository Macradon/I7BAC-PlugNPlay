import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NimRoutingModule } from './nim-routing.module';
import { NimComponent } from './nim.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [NimComponent],
  imports: [CommonModule, NimRoutingModule, SharedModule],
})
export class NimModule {}
