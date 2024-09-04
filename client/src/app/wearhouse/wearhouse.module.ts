import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MainwearhouseComponent } from './mainwearhouse/mainwearhouse.component';



@NgModule({
  declarations: [
    MainwearhouseComponent
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
  ],
  exports:[MainwearhouseComponent]
})
export class WearhouseModule { }
