import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisplayItemsComponent } from './display-items/display-items.component';
import { TableModule } from 'primeng/table';
import { BrowserModule } from '@angular/platform-browser';



@NgModule({
  declarations: [
    DisplayItemsComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    TableModule
  ]
})
export class ItemsModule { }
