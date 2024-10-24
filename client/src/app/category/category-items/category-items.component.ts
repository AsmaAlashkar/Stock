import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DialogService } from 'primeng/dynamicdialog';
import { TableLazyLoadEvent } from 'primeng/table';
import { CreateItemComponent } from 'src/app/items/create-item/create-item.component';
import { ItemsService } from 'src/app/items/items.service';
import { ItemDetailsDto, ItemDetailsResult } from 'src/app/shared/models/items';

@Component({
  selector: 'app-category-items',
  templateUrl: './category-items.component.html',
  styleUrls: ['./category-items.component.scss']
})
export class CategoryItemsComponent implements OnInit {

  ItemDetailsResult: ItemDetailsResult;
  ItemsDetails: ItemDetailsDto[] = [];
  pageSize: number = 5;
  pageNumber: number = 1;
  totalRecords: number = 0;
  loading: boolean = true;
  categoryId: number;

  constructor(
    private itemsService: ItemsService,
    private route: ActivatedRoute,
    private dialogService: DialogService

  ) {
    this.ItemDetailsResult = { itemsDetails: [], total: 0 };
    this.categoryId = 0;
  }

  ngOnInit(): void {
    console.log("gg",this.hasItems());

    this.route.paramMap.subscribe(params => {
      const catId = params.get('id');
      if (catId) {
        this.categoryId = +catId;
        this.getItems({ first: 0 }); // Fetch items for the given category
      }
    });
  }

  getItems(event: TableLazyLoadEvent) {

    const skip = event.first || 0;
    const currentPage = Math.floor(skip / this.pageSize) + 1;
  
    this.itemsService.getItemsByCategoryId(this.categoryId, this.pageSize, currentPage, skip).subscribe({
      next: (data) => {
         this.loading = false;
        this.ItemDetailsResult = data;
        this.ItemsDetails = this.ItemDetailsResult.itemsDetails;
        this.totalRecords = this.ItemDetailsResult.total;
      },
      error: (error) => {
        this.loading = false;
        console.error('Error fetching items', error);
      }
    });
  }
  openCreateItemModal(categoryId: number) {
    const catId = this.ItemsDetails[0]?.itemId;
    const dialogRef = this.dialogService.open(CreateItemComponent, {
      data: { categoryId },
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
  onPageChange(event: any) {
    // Update pageSize based on user selection
    this.pageSize = event.rows;
  
    // Call getItems with the updated page size and reset the current page number
    const skip = event.first || 0;
    const currentPage = Math.floor(skip / this.pageSize) + 1;
  
    this.getItems({ first: skip, rows: this.pageSize });
  }
  // Helper method to check if there are items
  hasItems(): boolean {
    return this.ItemsDetails && this.ItemsDetails.length > 0;
  }
}
