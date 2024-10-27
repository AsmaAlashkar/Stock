import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PermissionService } from '../permission.service';
import { Permissionaction } from 'src/app/shared/models/permissionaction';
import { WearhouseService } from 'src/app/wearhouse/wearhouse.service';
import { subWearhouseVM } from 'src/app/shared/models/subwearhouse';
import { ItemsService } from 'src/app/items/items.service';
import { ItemDetailsDtoVM, ItemDetailsPerTab } from 'src/app/shared/models/items';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-permission-action',
  templateUrl: './permission-action.component.html',
  styleUrls: ['./permission-action.component.scss']
})
export class PermissionActionComponent implements OnInit {

  permActForm!: FormGroup;
  errors: string[] = [];
  headerValue:string = '';
  Permissionaction: Permissionaction;
  ItemDetailsResultVM: ItemDetailsDtoVM[] = [];
  selectedItems!: ItemDetailsDtoVM[];
  subWearhouses: subWearhouseVM[] = [];
  itemDetailsPerTab:  ItemDetailsPerTab[] = [];
  selectedSubWearFrom!: subWearhouseVM;
  selectedSubWearTo!: subWearhouseVM;
  filteredItems: ItemDetailsDtoVM[] = [];

  constructor(
    private toastr: ToastrService,
    private permService: PermissionService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private activeRoute: ActivatedRoute,
    private wearhouseService: WearhouseService,
    private itemsService: ItemsService
  )
  {
    this.Permissionaction = {
      permId: 0,
      items: [{itemId: 0, destinationSubId: 0, quantity: 0, subId: 0}],
      permTypeFk: 0,
      permCreatedat: ""
    }
  }

  ngOnInit(): void {
    this.getSubwearhouse();

    this.headerValue = this.config.data.headerValue;
    console.log(this.headerValue);
  }

  getSubwearhouse() {
    this.wearhouseService.getsubWearhouseVM().subscribe({
      next: (data) => {
        this.subWearhouses = data.map(sub=>({
          subId: sub.subId, subName: sub.subName
        }));
        console.log("subWearhouses :", this.subWearhouses);
      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    }
    );
  }

  getItemsName() {
    this.itemsService.getItemsVM().subscribe({
      next: (data) => {
        this.ItemDetailsResultVM = data;
        console.log("ItemDetailsResultVM", this.ItemDetailsResultVM);
      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    });
  }

  getItemsNameBySubId() {
    const itemId = this.activeRoute.snapshot.paramMap.get('id');
    if (itemId) {
      const numItemId = +itemId;
      this.itemsService.getItemsBySubIdVM(numItemId).subscribe({
        next: (data) => {
          this.ItemDetailsResultVM = data;
          console.log("ItemDetailsResultVM", data);
        },
        error: (error) => {
          console.error('Error fetching items', error);
        }
      });
    }
  }

  onSubWearhouseChange() {
    // console.log("subId", this.selectedSubWearFrom.subId);
    if (this.selectedSubWearFrom) {
      this.displayItemsBySubId(this.selectedSubWearFrom.subId);
    } else {
      this.filteredItems = [];
      this.selectedItems = [];
    }
    this.getItemsBySubIdItemId(event);
  }

  displayItemsBySubId(subId: number) {
    console.log("subId:",subId);
    if (this.headerValue === "اضافة" || this.headerValue === "إضافة" || this.headerValue === "أضافة") {
      if (this.selectedSubWearFrom) {
      this.itemsService.getItemsVM().subscribe({
        next: (data) => {
          this.ItemDetailsResultVM = data;
          this.filteredItems = this.ItemDetailsResultVM;
          console.log("ItemDetailsResultVM", this.ItemDetailsResultVM);
        },
        error: (error) => {
          console.error('Error fetching items', error);
        }
      });
      } else {
        this.filteredItems = [];
      }
    } else {
      this.itemsService.getItemsBySubIdVM(subId).subscribe({
        next: (data) => {
          this.filteredItems = data;
          console.log("ItemDetailsResultVM", data);
        },
        error: (error) => {
          console.error('Error fetching items', error);
        }
      });
    }
  }

  getItemsBySubIdItemId($event:any) {
    if ($event.length > 0) {
      let subId = this.selectedSubWearFrom.subId;
      console.log("subId From getItemsBySubIdItemId",subId);

      let index = $event.length - 1;
      console.log('Selected item ID:', $event[index].itemId);
      let itemId = $event[index].itemId;

      this.itemsService.getItemsBySubIdItemId(subId, itemId).subscribe({
        next: (data) => {
          const exists = this.itemDetailsPerTab.some(item => item.itemId === data.itemId);
          if (!exists) {
            this.itemDetailsPerTab.push(data)
            console.log("ItemDetailsPerTab", data);
          }
        },
        error: (error) => {
          console.error('Error fetching items', error);
        }
      });
    } else {
      console.log('No items selected.');
    }
  }

  onItemUncheck(item: ItemDetailsDtoVM) {
    this.itemDetailsPerTab = this.itemDetailsPerTab.filter(i => i.itemId !== item.itemId);
    console.log("Item removed:", item);
  }

  // onMultiSelectChange(event: any) {
  //   if (!this.selectedItems) {
  //     this.selectedItems = [];
  //   }
  //   const removedItems = this.selectedItems.filter(item =>
  //     !this.itemDetailsPerTab || !this.itemDetailsPerTab.some(i => i.itemId === item.itemId)
  //   );

  //   removedItems.forEach(item => this.onItemUncheck(item));

  //   this.getItemsBySubIdItemId(this.selectedItems);
  // }

  onMultiSelectChange(event: any) {
    // Ensure selectedItems is an array
    if (!this.selectedItems) {
      this.selectedItems = [];
    }

    // Find removed items
    const removedItems = this.itemDetailsPerTab.filter(item =>
      !this.selectedItems.some(selected => selected.itemId === item.itemId)
    );

    // Remove unchecked items from itemDetailsPerTab
    removedItems.forEach(item => this.onItemUncheck(item));

    // Handle newly selected items
    this.getItemsBySubIdItemId(this.selectedItems);
  }

}
