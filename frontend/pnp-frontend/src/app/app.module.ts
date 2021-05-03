import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FrontPageComponent } from './frontPage/front-page/front-page.component';
import { SocialModule } from './social/social.module';

@NgModule({
  declarations: [AppComponent, FrontPageComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    CommonModule,
    FlexLayoutModule,
    SocialModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
