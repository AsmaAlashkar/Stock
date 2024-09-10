import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MainwearhouseComponent } from './mainwearhouse/mainwearhouse.component';
import { MatCardModule } from '@angular/material/card';
import { SubwearhouseComponent } from './subwearhouse/subwearhouse.component';
import { RouterModule } from '@angular/router';
import { MainwearhouseDetalisComponent } from './mainwearhouse-detalis/mainwearhouse-detalis.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';



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
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 3000, // Set the duration in milliseconds
      positionClass: 'toast-bottom-right', // Set the position
      preventDuplicates: false, // Prevent duplicate messages
      
    }),
    
  ],
  exports:[MainwearhouseComponent,SubwearhouseComponent]
})
export class WearhouseModule { }
