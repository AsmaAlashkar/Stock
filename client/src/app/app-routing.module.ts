import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { SubwearhouseComponent } from './wearhouse/subwearhouse/subwearhouse.component';
import { WearhouseModule } from './wearhouse/wearhouse.module';
import { MainwearhouseComponent } from './wearhouse/mainwearhouse/mainwearhouse.component';
import { MainwearhouseDetalisComponent } from './wearhouse/mainwearhouse-detalis/mainwearhouse-detalis.component';
import { HomeComponent } from './homepage/home/home.component';
import { SubwearhouseDetailsComponent } from './wearhouse/subwearhouse-details/subwearhouse-details.component';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { DisplayCategoriesComponent } from './category/display-categories/display-categories.component';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'mainwearhouse', component:MainwearhouseComponent},
  {path:'mainwearhouse/:id', component:MainwearhouseDetalisComponent},
  {path:'Subwearhouse', component:SubwearhouseComponent},
  {path:'Subwearhouse/:id', component:SubwearhouseComponent},
  {path:'Subwearhouse-details/:id', component:SubwearhouseDetailsComponent},
  {path:'categories',component: DisplayCategoriesComponent},
  {path:'login', component:LoginComponent},
  {path:'register', component:RegisterComponent},
  {path:'**', redirectTo:'', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
