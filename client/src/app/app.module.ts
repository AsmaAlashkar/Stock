import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { MatInputModule } from '@angular/material/input';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { WearhouseModule } from './wearhouse/wearhouse.module';
import { HttpClientModule } from '@angular/common/http';
import {MatCardModule} from '@angular/material/card';
import { HomepageModule } from './homepage/homepage.module';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    WearhouseModule,
    HomepageModule,
    MatInputModule,
    BsDropdownModule.forRoot(),
    MatCardModule,
    ToastrModule.forRoot({
      timeOut: 3000, // Set the duration in milliseconds
      positionClass: 'toast-bottom-right', // Set the position
      preventDuplicates: false, // Prevent duplicate messages
      
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
