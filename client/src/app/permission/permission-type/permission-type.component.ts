import { Component } from '@angular/core';
import { PermissionService } from '../permission.service';
import { IPermissionType } from 'src/app/shared/models/permissiontype';

@Component({
  selector: 'app-permissin-type',
  templateUrl: './permission-type.component.html',
  styleUrls: ['./permission-type.component.scss']
})
export class PermissinTypeComponent {

  permissionTypes: IPermissionType[] = [];
  filteredPermissionTypes: IPermissionType[] = [];
  filterName: string = ''; // Variable to hold the filter input value

  constructor(private permissionservice: PermissionService) {}

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

}
