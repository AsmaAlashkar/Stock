import { Component } from '@angular/core';
import { Table } from 'primeng/table';
import { DisplayAllPermission } from 'src/app/shared/models/permissionaction';
import { PermissionService } from '../permission.service';

@Component({
  selector: 'app-display-all-permissions',
  templateUrl: './display-all-permissions.component.html',
  styleUrls: ['./display-all-permissions.component.scss']
})
export class DisplayAllPermissionsComponent {

  displayAllPermission: DisplayAllPermission[] = [];
  //   representatives!: Representative[];

  //   statuses!: any[];

    loading: boolean = true;

  //   activityValues: number[] = [0, 100];

    constructor(private permissionService: PermissionService) {}

    // ngOnInit() {
    //     this.permissionService.getAllPermissions().then((displayAllPermission) => {
    //         this.displayAllPermission = displayAllPermission;
    //         this.loading = false;

    //         this.displayAllPermission.forEach((displayAllPermission) => (displayAllPermission.date = new Date(<Date>displayAllPermission.date)));
    //     });
    // }

    clear(table: Table) {
        table.clear();
    }

  //   getSeverity(status: string) {
  //       switch (status.toLowerCase()) {
  //           case 'unqualified':
  //               return 'danger';

  //           case 'qualified':
  //               return 'success';

  //           case 'new':
  //               return 'info';

  //           case 'negotiation':
  //               return 'warning';

  //           case 'renewal':
  //               return null;
  //       }
  //   }

}
