import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TreeModule } from 'primeng/tree';
import { CardModule } from 'primeng/card';
import { DisplayCategoriesComponent } from './display-categories/display-categories.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { CreateCategoryComponent } from './create-category/create-category.component';


@NgModule({
  declarations: [
    DisplayCategoriesComponent,
    CreateCategoryComponent
  ],
  imports: [
    CommonModule,
    TreeModule,
    CardModule,
    CommonModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    MatCardModule,
    RouterModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 3000, // Set the duration in milliseconds
      positionClass: 'toast-bottom-right', // Set the position
      preventDuplicates: false, // Prevent duplicate messages
      
    }),
    DialogModule,
    DynamicDialogModule,
    MatButtonModule,
    ButtonModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CategoryModule { }
