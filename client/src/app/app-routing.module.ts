import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SubwearhouseComponent } from './wearhouse/subwearhouse/subwearhouse.component';
import { WearhouseModule } from './wearhouse/wearhouse.module';
import { MainwearhouseComponent } from './wearhouse/mainwearhouse/mainwearhouse.component';

const routes: Routes = [
  {path:'', component:MainwearhouseComponent},
  {path:'mainwearhouse/:id', component:MainwearhouseComponent},
  {path:'Subwearhouse', component:SubwearhouseComponent},
  {path:'Subwearhouse/:id', component:SubwearhouseComponent},
  {path:'**', redirectTo:'', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
