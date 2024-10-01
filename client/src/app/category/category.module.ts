import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';

import { DisplayCategoriesComponent } from './display-categories/display-categories.component';


@NgModule({
  declarations: [
    DisplayCategoriesComponent
  ],
  imports: [
    CommonModule,
    TableModule
  ]
})
export class CategoryModule { }
