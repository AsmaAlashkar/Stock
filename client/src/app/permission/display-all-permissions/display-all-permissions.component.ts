import { Component } from '@angular/core';
import { Table, TableLazyLoadEvent } from 'primeng/table';
import { DisplayAllPermission, DisplayAllPermissionVM } from 'src/app/shared/models/permissionaction';
import { PermissionService } from '../permission.service';
import { IPermissionType } from 'src/app/shared/models/permissiontype';

@Component({
  selector: 'app-display-all-permissions',
  templateUrl: './display-all-permissions.component.html',
  styleUrls: ['./display-all-permissions.component.scss']
})
export class DisplayAllPermissionsComponent {

  displayAllPermVM: DisplayAllPermissionVM[] = [];
  PermVM!: DisplayAllPermissionVM;
  displayAllPerm!: DisplayAllPermission;
  pageNumber: number = 1;
  pageSize: number = 10;
  totalRecords: number = 0;
  rowsPerPageOptions = [ 10, 20];
  loading: boolean = true;

  permissionTypes: IPermissionType[] = [];
  selectedPerm!: IPermissionType;
  selectedDate: Date | null = null;

  constructor(private permissionService: PermissionService, private permissionservice: PermissionService) {
    this.displayAllPerm = { values: [], totalRecords: 0, totalPages: 0, pageNumber: 1, pageSize: 10 };
  }

  ngOnInit() {
    this.loadPermissionTypes();


  }

  getAllPermissions($event: TableLazyLoadEvent) {
    const totalRecords = $event.first || 0;
    const pageNumber = Math.floor(totalRecords / this.pageSize) + 1;

    this.permissionService.getAllPermissions(pageNumber, this.pageSize).subscribe({
      next: (data) => {
        this.loading = false;
        this.displayAllPermVM = data.values;
        this.totalRecords = data.totalRecords;
        this.PermVM = data.values[0]
        console.log( this.PermVM);
      },
      error: (error) => {
        this.loading = false;
        console.error('Error fetching items', error);
      }
    });
  }

  loadPermissionTypes(): void {
    this.permissionservice.getPermissionTypes().subscribe({
      next: (data) => {
        this.permissionTypes = data;
      },
      error: (error) => {
        console.error('Error fetching permission types', error);
      }
    });
  }

  onDropdownChange(selectedType: IPermissionType): void {
    if (selectedType && selectedType.perId) {
      this.getPermissionsByType(selectedType.perId);
    }
  }

  getPermissionsByType(typeId: number) {
    this.permissionService.getPermissionsByTypeId(typeId).subscribe({
      next: (data) => {
        this.loading = false;
        this.displayAllPermVM = data;
        // this.totalRecords = data.length;
      },
      error: (error) => {
        this.loading = false;
        console.error('Error fetching permissions by type', error);
      }
    });
  }

  getPermissionsByDate(date: string) {
    console.log("Fetching permissions for date:", date);
    this.permissionService.getPermissionsByDate(date).subscribe({
      next: (data) => {
        console.log("Permissions Data:", data);
        this.loading = false;
        this.displayAllPermVM = data;
        this.totalRecords = data.length;
      },
      error: (error) => {
        this.loading = false;
        console.error('Error fetching permissions by date', error);
      }
    });
  }

  onDateChange(event: any): void {
    if (event) {
      const selectedDate = this.formatDate(event);
      console.log("Selected Date: ", selectedDate);

      this.permissionService.getPermissionsByDate(selectedDate).subscribe({
        next: (data) => {
          console.log("Permissions Data:", data);
          this.loading = false;
          this.displayAllPermVM = data;
          this.totalRecords = data.length;
        },
        error: (error) => {
          this.loading = false;
          console.error('Error fetching permissions by date', error);
        }
      });
    }
  }

  /*onDateChange(event: any): void {
    console.log("Date selected:", event);
    if (event) {
      const selectedDate = this.formatDate(event);
      console.log("Formatted Date:", selectedDate);

      this.getPermissionsByDate(selectedDate);
    }
  }*/

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  clear(table: Table) {
      table.clear();
  }

}
