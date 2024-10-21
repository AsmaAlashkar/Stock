import { Component } from '@angular/core';
import { ItemDetailsDto, ItemDetailsResult } from 'src/app/shared/models/items';
import { ItemsService } from '../items.service';
import { TableLazyLoadEvent } from 'primeng/table';

@Component({
  selector: 'app-display-items',
  templateUrl: './display-items.component.html',
  styleUrls: ['./display-items.component.scss']
})
export class DisplayItemsComponent {
  ItemDetailsResult: ItemDetailsResult;
  ItemsDetails: ItemDetailsDto[] = [];
  pageSize: number = 10; // Default page size
  totalRecords: number = 0;
  loading: boolean = true;
  currentPage: number = 1; // Keep track of current page
  rowsPerPageOptions = [ 10, 20]; // Dropdown options for page size

  constructor(private itemsService: ItemsService) {
    this.ItemDetailsResult = { itemsDetails: [], total: 0 };
  }

  getItems(event: TableLazyLoadEvent) {
    const skip = event.first || 0; // Calculate the number of records to skip
    const currentPage = Math.floor(skip / this.pageSize) + 1; // Calculate the correct page number

    // Fetch items from the backend using the calculated values
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

  onPageSizeChange(event: any) {
    const newSize = event.target.value; // Extract the selected value from the event
    this.pageSize = Number(newSize); // Convert it to a number and set the page size
    this.currentPage = 1; // Reset to the first page
    this.getItems({ first: 0 }); // Reload items for the new page size
  }
}
