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
    ReactiveFormsModule
  ],
  providers: [
    DialogService,
    MessageService,
    BrowserModule,
    TableModule
  ]
})
export class PermissionModule { }
