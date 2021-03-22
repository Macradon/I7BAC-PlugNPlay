import { NgModule } from '@angular/core';
import { SharedRoutingModule } from './shared-routing.module';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [NavbarComponent],
  imports: [SharedRoutingModule],
  exports: [NavbarComponent],
})
export class SharedModule {}
