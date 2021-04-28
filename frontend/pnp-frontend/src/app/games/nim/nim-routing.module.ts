import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NimComponent } from './nim.component';

const routes: Routes = [  { path: '', component: NimComponent },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NimRoutingModule { }
