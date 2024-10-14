import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissinTypeComponent } from './permission-type/permission-type.component';
import { CreatePermissionComponent } from './create-permission/create-permission.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    PermissinTypeComponent,
    CreatePermissionComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class PermissionModule { }
