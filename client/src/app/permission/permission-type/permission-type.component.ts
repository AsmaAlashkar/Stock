import { Component, OnDestroy } from '@angular/core';
import { PermissionService } from '../permission.service';
import { IPermissionType } from 'src/app/shared/models/permissiontype';
import { MessageService } from 'primeng/api';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { PermissionActionComponent } from '../permission-action/permission-action.component';

@Component({
  selector: 'app-permissin-type',
  templateUrl: './permission-type.component.html',
  styleUrls: ['./permission-type.component.scss']
})
export class PermissinTypeComponent implements OnDestroy{

  permissionTypes: IPermissionType[] = [];
  filteredPermissionTypes: IPermissionType[] = [];
  filterName: string = ''; // Variable to hold the filter input value

  ref: DynamicDialogRef | undefined;

  constructor(private permissionservice: PermissionService, private dialogService: DialogService, private messageService: MessageService) {}

  ngOnInit(): void {
    this.loadPermissionTypes(); // Call the method on component initialization
  }

  loadPermissionTypes(): void {
    this.permissionservice.getPermissionTypes().subscribe(
      (data: IPermissionType[]) => {
        this.permissionTypes = data;
        this.filteredPermissionTypes = data; // Initialize the filtered array with all items
      },
      (error) => {
        console.error('Error fetching permission types', error);
      }
    );
  }

  applyFilter(): void {
    if (this.filterName.trim() === '') {
      this.filteredPermissionTypes = this.permissionTypes; // Reset to all items if no filter
    } else {
      this.filteredPermissionTypes = this.permissionTypes.filter((permissionType) =>
        permissionType.perTypeValue.toLowerCase().includes(this.filterName.toLowerCase())
      );
    }
  }

  onCardClick(permissionType: IPermissionType): void {
    console.log('Card clicked:', permissionType.perId);
    this.show(permissionType);
  }

  show(selectedPermissionType: IPermissionType) {
    const headerValue = selectedPermissionType ? selectedPermissionType.perTypeValue : 'Default Header';
    this.ref = this.dialogService.open(PermissionActionComponent, {
      header: headerValue,
      width: '70%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      maximizable: true,
      data: {
        perId: selectedPermissionType.perId
      }
      // this.ref.onClose.subscribe((product: Product) => {
      //   if (product) {
      //       this.messageService.add({ severity: 'info', summary: 'Product Selected', detail: product.name });
      //   }
      // });
    });

    this.ref.onMaximize.subscribe((value) => {
      this.messageService.add({ severity: 'info', summary: 'Maximized', detail: `maximized: ${value.maximized}` });
    });
  }


  ngOnDestroy() {
      if (this.ref) {
          this.ref.close();
      }
  }
}
