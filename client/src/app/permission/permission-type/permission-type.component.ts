import { Component, OnDestroy } from '@angular/core';
import { PermissionService } from '../permission.service';
import { IPermissionType } from 'src/app/shared/models/permissiontype';
import { MessageService } from 'primeng/api';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { PermissionActionComponent } from '../permission-action/permission-action.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-permissin-type',
  templateUrl: './permission-type.component.html',
  styleUrls: ['./permission-type.component.scss']
})
export class PermissinTypeComponent implements OnDestroy{

  permissionTypes: IPermissionType[] = [];
  selectedPerm!: IPermissionType;
  filteredPermissionTypes: IPermissionType[] = [];
  filterName: string = '';

  ref: DynamicDialogRef | undefined;

  constructor(private permissionservice: PermissionService,
    private dialogService: DialogService,
    private messageService: MessageService,
    private router: Router) {}

  ngOnInit(): void {
    this.loadPermissionTypes();
  }

  loadPermissionTypes(): void {
    this.permissionservice.getPermissionTypes().subscribe({
      next: (data: IPermissionType[]) => {
        this.permissionTypes = data;
        this.filteredPermissionTypes = data;
      },
      error: (error) => {
        console.error('Error fetching permission types', error);
      }
    });
  }

  applyFilter(): void {
    if (this.filterName.trim() === '') {
      this.filteredPermissionTypes = this.permissionTypes;
    } else {
      this.filteredPermissionTypes = this.permissionTypes.filter((permissionType) =>
        permissionType.perTypeValueEn.toLowerCase().includes(this.filterName.toLowerCase())
      );
    }
  }

  onCardClick(permissionType: IPermissionType): void {
    // console.log('Card clicked:', permissionType.perId);
    this.show(permissionType);
  }

  show(selectedPermissionType: IPermissionType) {
    console.log("selectedPermissionType :", selectedPermissionType);
    const headerValue = selectedPermissionType ? selectedPermissionType.perTypeValueEn : 'Default Header';

    this.ref = this.dialogService.open(PermissionActionComponent, {
        header: headerValue,
        width: '70%',
        contentStyle: { overflow: 'auto' },
        baseZIndex: 10000,
        maximizable: true,
        data: {
          headerValue: headerValue,
          perId: selectedPermissionType.perId
        }
    });

    this.ref.onClose.subscribe((permissionTypes: IPermissionType) => {
        if (permissionTypes) {
            this.messageService.add({
                severity: 'info',
                summary: 'Product Selected',
                detail: permissionTypes.perTypeValueEn
            });
        }
    });

    this.ref.onMaximize.subscribe((value) => {
        this.messageService.add({
            severity: 'info',
            summary: 'Maximized',
            detail: `maximized: ${value.maximized}`
        });
    });
  }

  ngOnDestroy() {
      if (this.ref) {
          this.ref.close();
      }
  }

  onDropdownChange(selectedPermission: IPermissionType): void {
    if (selectedPermission) {
        this.onCardClick(selectedPermission);
    }
  }


  openAllPermissions()
  {
    this.router.navigate(['/allPermissions']);
  }
}
