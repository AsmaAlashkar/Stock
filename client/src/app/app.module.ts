import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { MatInputModule } from '@angular/material/input';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { WearhouseModule } from './wearhouse/wearhouse.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {MatCardModule} from '@angular/material/card';
import { HomepageModule } from './homepage/homepage.module';
import { ToastrModule } from 'ngx-toastr';
import { DialogModule } from 'primeng/dialog';  // PrimeNG Dialog module
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { ButtonModule } from 'primeng/button';  // Ensure ButtonModule is also imported if needed
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptors';
import { AccountModule } from './account/account.module';
import { CategoryModule } from './category/category.module';
import { ItemsModule } from './items/items.module';
import { PermissionModule } from './permission/permission.module';
import { ChatbotModule } from './chatbot/chatbot.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    CoreModule,
    AccountModule,
    ChatbotModule,
    WearhouseModule,
    CategoryModule,
    HomepageModule,
    PermissionModule,
    ItemsModule,
    MatInputModule,
    BsDropdownModule.forRoot(),
    MatCardModule,
    ToastrModule.forRoot({
      timeOut: 3000, // Set the duration in milliseconds
      positionClass: 'toast-bottom-right', // Set the position
      preventDuplicates: false, // Prevent duplicate messages

    }),
    DialogModule,
    DynamicDialogModule,
    ButtonModule,  // Import ButtonModule if using PrimeNG buttons
    MatButtonModule,
    NgxSpinnerModule,
  ],
  providers: [DialogService,
    {provide:HTTP_INTERCEPTORS, useClass: LoadingInterceptor,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

