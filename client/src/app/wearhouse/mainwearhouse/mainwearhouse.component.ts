import { Component } from '@angular/core';
import { IMainWearhouse } from 'src/app/shared/models/wearhouse';
import { WearhouseService } from '../wearhouse.service';
import { DialogService } from 'primeng/dynamicdialog';
import { MainModalComponent } from '../main-modal/main-modal.component';

@Component({
  selector: 'app-mainwearhouse',
  templateUrl: './mainwearhouse.component.html',
  styleUrls: ['./mainwearhouse.component.scss']
})
export class MainwearhouseComponent {
  mhouses!: IMainWearhouse[];

  constructor(private mainwearService: WearhouseService, 
              private dialogService: DialogService) {}

  ngOnInit(): void {
    this.loadhouses();
  }

  loadhouses() {
    this.mainwearService.getmainwearhouse().subscribe(
      (data: IMainWearhouse[]) => {
        this.mhouses = data;
      },
      error => {
        console.log(error);
      }
    );
  }

  openCreateWarehouseModal() {
    this.dialogService.open(MainModalComponent, {
      header: 'Create New Main Warehouse',
      width: '70%',
      contentStyle: { 'max-height': '80vh', overflow: 'auto' }
    });
  }
}
