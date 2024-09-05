import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MainwearhouseComponent } from './mainwearhouse/mainwearhouse.component';
import { MatCardModule } from '@angular/material/card';



@NgModule({
  declarations: [
    MainwearhouseComponent
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    MatCardModule,
  ],
  exports:[MainwearhouseComponent]
})
export class WearhouseModule { }
