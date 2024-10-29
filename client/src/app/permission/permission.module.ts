import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissinTypeComponent } from './permission-type/permission-type.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PermissionActionComponent } from './permission-action/permission-action.component';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { BrowserModule } from '@angular/platform-browser';
import { TableModule } from 'primeng/table';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    PermissinTypeComponent,
    PermissionActionComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    DynamicDialogModule,
    InputNumberModule,
    ReactiveFormsModule,
    DropdownModule,
    MultiSelectModule,
    BrowserAnimationsModule,
    TableModule,
  ],
  providers: [
    DialogService,
    MessageService,
    BrowserModule
  ]
})
export class PermissionModule { }
