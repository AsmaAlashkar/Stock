import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MainwearhouseComponent } from './mainwearhouse/mainwearhouse.component';
import { MatCardModule } from '@angular/material/card';
import { SubwearhouseComponent } from './subwearhouse/subwearhouse.component';
import { RouterModule } from '@angular/router';
import { MainwearhouseDetalisComponent } from './mainwearhouse-detalis/mainwearhouse-detalis.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    MainwearhouseComponent,
    SubwearhouseComponent,
    MainwearhouseDetalisComponent
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    MatCardModule,
    RouterModule,
    ReactiveFormsModule
    
  ],
  exports:[MainwearhouseComponent,SubwearhouseComponent]
})
export class WearhouseModule { }
