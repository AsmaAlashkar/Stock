import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';




@NgModule({
  declarations: [
    NavBarComponent
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    RouterModule,
    SharedModule

  ],
  exports:[NavBarComponent, BsDropdownModule]
})
export class CoreModule { }
