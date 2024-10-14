import { Component } from '@angular/core';
import {  ItemDetailsDto, ItemDetailsResult } from 'src/app/shared/models/items';
import { ItemsService } from '../items.service';
import { TableLazyLoadEvent } from 'primeng/table';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-display-items',
  templateUrl: './display-items.component.html',
  styleUrls: ['./display-items.component.scss']
})
export class DisplayItemsComponent {
  ItemDetailsResult:ItemDetailsResult
  ItemsDetails: ItemDetailsDto[] = [];
  pageSize: number = 10;
  pageNumber: number = 1;
  totalRecords: number = 0;
  loading: boolean = true;

  constructor(private itemsService: ItemsService)
  {
    this.ItemDetailsResult = {itemsDetails:[], total:0}
  }

  getItems(event: TableLazyLoadEvent) {

    const skip = event.first || 0;
    const currentPage = Math.floor(skip / this.pageSize) + 1;

    this.itemsService.getItems(this.pageSize, currentPage, skip).subscribe({
      next: (data) => {
        this.loading = false;
        this.ItemDetailsResult = data;
        this.ItemsDetails= this.ItemDetailsResult.itemsDetails;

      },
      error: (error) => {
        this.loading = false;
        console.error('Error fetching items', error);
      }
    }
    );
  }
}
