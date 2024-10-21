import { Component } from '@angular/core';
import { ItemDetailsDto, ItemDetailsResult } from 'src/app/shared/models/items';
import { ItemsService } from '../items.service';
import { TableLazyLoadEvent } from 'primeng/table';
import { CreateItemComponent } from '../create-item/create-item.component';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-display-items',
  templateUrl: './display-items.component.html',
  styleUrls: ['./display-items.component.scss']
})
export class DisplayItemsComponent {

  ItemDetailsResult: ItemDetailsResult;
  ItemsDetails: ItemDetailsDto[] = [];
  pageSize: number = 10; 
  totalRecords: number = 0;
  loading: boolean = true;
  currentPage: number = 1; 
  rowsPerPageOptions = [ 10, 20]; 

  constructor(private itemsService: ItemsService,
    private dialogService: DialogService

  ) {
    this.ItemDetailsResult = { itemsDetails: [], total: 0 };
  }

  getItems(event: TableLazyLoadEvent) {
    const skip = event.first || 0; 
    const currentPage = Math.floor(skip / this.pageSize) + 1; 

    this.itemsService.getItems(this.pageSize, currentPage, skip).subscribe({
      next: (data) => {
        this.loading = false;
        this.ItemDetailsResult = data;
        this.ItemsDetails= this.ItemDetailsResult.itemsDetails;
        console.log("data",data);

      },
      error: (error) => {
        this.loading = false;
        console.error('Error fetching items', error);
      }
    });
  }

  openCreateItemModal() {
    const catId = this.ItemsDetails[0]?.itemId;
    const dialogRef = this.dialogService.open(CreateItemComponent, {
      data: { catId: catId },
      header: 'Create New Item',
      width: '70%',
      contentStyle: { 'max-height': '80vh', overflow: 'auto' }
    });
    dialogRef.onClose.subscribe((result) => {
      if (result === 'confirmed') {
        // this.getItems($event);
      }
    });
  }
  onPageSizeChange(event: any) {
    const newSize = event.target.value; // Extract the selected value from the event
    this.pageSize = Number(newSize); // Convert it to a number and set the page size
    this.currentPage = 1; // Reset to the first page
    this.getItems({ first: 0 }); // Reload items for the new page size
  }
}
