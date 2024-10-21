import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisplayItemsComponent } from './display-items/display-items.component';
import { TableModule } from 'primeng/table';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateItemComponent } from './create-item/create-item.component';



@NgModule({
  declarations: [
    DisplayItemsComponent,
    CreateItemComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    TableModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ItemsModule { }
