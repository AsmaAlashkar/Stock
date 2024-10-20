import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TableLazyLoadEvent } from 'primeng/table';
import { ItemsService } from 'src/app/items/items.service';
import { ItemDetailsDto, ItemDetailsResult } from 'src/app/shared/models/items';

@Component({
  selector: 'app-sub-items',
  templateUrl: './sub-items.component.html',
  styleUrls: ['./sub-items.component.scss']
})
export class SubItemsComponent implements OnInit {
  ItemDetailsResult: ItemDetailsResult;
  ItemsDetails: ItemDetailsDto[] = [];
  pageSize: number = 5;
  pageNumber: number = 1;
  totalRecords: number = 0;
  loading: boolean = true;
  categoryId: number;

  constructor(
    private itemsService: ItemsService,
    private route: ActivatedRoute
  ) {
    this.ItemDetailsResult = { itemsDetails: [], total: 0 };
    this.categoryId = 0;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const catId = params.get('id');
      if (catId) {
        this.categoryId = +catId;
        this.getItems({ first: 0 }); // Fetch items when the category ID is set
      }
    });
  }

  getItems(event: TableLazyLoadEvent) {
    const skip = event.first || 0;
    const currentPage = Math.floor(skip / this.pageSize) + 1;

    this.loading = true; // Start loading
    this.itemsService.getItemsBySubId(this.categoryId, this.pageSize, currentPage, skip).subscribe({
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

  // Helper method to check if there are items
  hasItems(): boolean {
    return this.ItemsDetails && this.ItemsDetails.length > 0;
  }
}
