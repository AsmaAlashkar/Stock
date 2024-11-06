import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissinTypeComponent } from './permission-type/permission-type.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PermissionActionComponent } from './permission-action/permission-action.component';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BrowserModule } from '@angular/platform-browser';
import { TableModule } from 'primeng/table';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DisplayAllPermissionsComponent } from './display-all-permissions/display-all-permissions.component';
import { RouterModule } from '@angular/router';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessagesModule } from 'primeng/messages';
import { ToastModule } from 'primeng/toast';
import { CreatePermissionComponent } from './create-permission/create-permission.component';
import { SplitButtonModule } from 'primeng/splitbutton';
import { CalendarModule } from 'primeng/calendar';

@NgModule({
  declarations: [
    PermissinTypeComponent,
    PermissionActionComponent,
    DisplayAllPermissionsComponent,
    CreatePermissionComponent
  ],
  imports: [
    CommonModule,
    RouterModule ,
    FormsModule,
    DynamicDialogModule,
    InputNumberModule,
    ReactiveFormsModule,
    DropdownModule,
    MultiSelectModule,
    BrowserAnimationsModule,
    TableModule,
    ConfirmDialogModule,
    MessagesModule,
    ToastModule,
    SplitButtonModule,
    CalendarModule
  ],
  providers: [
    DialogService,
    MessageService,
    BrowserModule,
    ConfirmationService
  ]
})
export class PermissionModule { }
