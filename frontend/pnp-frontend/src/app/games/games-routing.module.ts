import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QueueComponent } from './queue/queue.component';

const routes: Routes = [
  { path: 'queue/:id', component: QueueComponent },
  {
    path: 'nim',
    loadChildren: () => import('./nim/nim.module').then((m) => m.NimModule),
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GamesRoutingModule {}
