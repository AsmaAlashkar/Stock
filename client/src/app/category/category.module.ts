import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TreeModule } from 'primeng/tree';
import { CardModule } from 'primeng/card';
import { DisplayCategoriesComponent } from './display-categories/display-categories.component';
import { CreateCategoryComponent } from './create-category/create-category.component';


@NgModule({
  declarations: [
    DisplayCategoriesComponent,
    CreateCategoryComponent
  ],
  imports: [
    CommonModule,
    TreeModule,
    CardModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class CategoryModule { }
