import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WorldTimeComponent } from './pages/time/world-time.component';

const routes: Routes = [

  { path: '', component: WorldTimeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
