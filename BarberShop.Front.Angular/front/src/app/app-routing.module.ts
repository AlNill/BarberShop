import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import {BarbersComponent} from './barbers/barbers.component'


const routes: Routes = [
  {path: 'barbers', component:BarbersComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
