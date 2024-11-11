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
import { authGuard } from './core/guards/auth.guard';
import { CategoryDetailsComponent } from './category/category-details/category-details.component';
import { DisplayItemsComponent } from './items/display-items/display-items.component';
import { PermissinTypeComponent } from './permission/permission-type/permission-type.component';
import { CategoryItemsComponent } from './category/category-items/category-items.component';
import { SubItemsComponent } from './wearhouse/sub-items/sub-items.component';
import { CreateItemComponent } from './items/create-item/create-item.component';
import { DisplayAllPermissionsComponent } from './permission/display-all-permissions/display-all-permissions.component';
import { CreatePermissionComponent } from './permission/create-permission/create-permission.component';
import { GenralReportComponent } from './reports/genral-report/genral-report.component';
import { MainReportComponent } from './reports/main-report/main-report.component';
import { ChatbotComponent } from './chatbot/chatbot/chatbot.component';

const routes: Routes = [
  {path:'', component:HomeComponent},
  {path:'mainwearhouse', component:MainwearhouseComponent},
  {path:'mainwearhouse/:id', component:MainwearhouseDetalisComponent},
  {path:'Subwearhouse', component:SubwearhouseComponent},
  {path:'Subwearhouse/:id', component:SubwearhouseComponent},
  {path:'Subwearhouse-details/:id', component:SubwearhouseDetailsComponent},
  {path:'viewSubItems/:id', component:SubItemsComponent},


  {path:'categories',component: DisplayCategoriesComponent},
  // {path:'category-details',component: CategoryDetailsComponent},
  {path:'category-details/:id',component: CategoryDetailsComponent},
  {path:'viewCategoryItems/:id',component: CategoryItemsComponent},


  {path:'displayAllPermission',component: PermissinTypeComponent},
  {path:'permissionDetails/:id',component: CategoryDetailsComponent},
  // {path:'CreatePermission',component: CreatePermissionComponent},

  {path:'items',component: DisplayItemsComponent},
  // {path:'createItem',component:CreateItemComponent},

  {path: 'reports', loadChildren: () => import('./reports/reports.module').then(m => m.ReportsModule)},

  // {path:'reports',component: },
  {path:'chatbot',component: ChatbotComponent},

  
  {path:'login', component:LoginComponent},
  {path:'register', component:RegisterComponent},
  {path:'**', redirectTo:'', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
