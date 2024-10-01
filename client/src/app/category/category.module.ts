import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TreeModule } from 'primeng/tree';
import { CardModule } from 'primeng/card';
import { DisplayCategoriesComponent } from './display-categories/display-categories.component';


@NgModule({
  declarations: [
    DisplayCategoriesComponent
  ],
  imports: [
    CommonModule,
    TreeModule,
    CardModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CategoryModule { }
