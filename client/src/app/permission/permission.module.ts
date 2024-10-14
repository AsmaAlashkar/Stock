import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissinTypeComponent } from './permission-type/permission-type.component';
import { CreatePermissionComponent } from './create-permission/create-permission.component';



@NgModule({
  declarations: [
    PermissinTypeComponent,
    CreatePermissionComponent
  ],
  imports: [
    CommonModule
  ]
})
export class PermissionModule { }
