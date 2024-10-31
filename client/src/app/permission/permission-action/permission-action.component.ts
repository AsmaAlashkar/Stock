import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
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
  showWarning: boolean = false;
  perId!: number;
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

  constructor (
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
      permTypeFk: 0,
      subId: 0,
      destinationSubId: null,
      items: [{itemId: 0, quantity: 0}],
      permCreatedat: ""
    },
    this.perId = this.config.data?.perId;
  }

  ngOnInit(): void {
    this.getSubwearhouse();

    this.headerValue = this.config.data.headerValue;
    console.log(this.headerValue);

    console.log('Permission Type ID:', this.perId);
  }

  onQuantityChange(item: ItemDetailsPerTab) {
    console.log('Current Quantity Updated:', item.currentQuantity);
    console.log('ItemDetailsPerTab:', this.itemDetailsPerTab);
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
    this.showWarning = !this.selectedSubWearFrom || !this.selectedSubWearTo;
  }

  getFilteredSubWearhousesForTo() {
    return this.subWearhouses.filter(sub =>
      sub.subId !== this.selectedSubWearTo?.subId
    );
  }

  getFilteredSubWearhousesForFrom() {
    return this.subWearhouses.filter(sub =>
      sub.subId !== this.selectedSubWearFrom?.subId
    );
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

      console.log("event", $event);
      console.log("this.ItemDetailsResultVM.length", this.ItemDetailsResultVM.length);
      // if ($event.length == this.ItemDetailsResultVM.length) {


      // }
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
      console.log('No SubWearHouse or items selected.');
    }
  }

  onItemUncheck(item: ItemDetailsDtoVM) {
    this.itemDetailsPerTab = this.itemDetailsPerTab.filter(i => i.itemId !== item.itemId);
    console.log("Item removed:", item);
  }

  onMultiSelectChange(event: any) {
    if (!this.selectedItems) {
        this.selectedItems = [];
    }

    if (event && Array.isArray(event)) {
      if (event.length === this.filteredItems.length) {
          this.selectedItems.forEach(item => {
              console.log("Selected Item ID: ", item.itemId);
              this.itemsService.getItemsBySubIdItemId(this.selectedSubWearFrom.subId, item.itemId).subscribe({
                  next: (data) => {
                      const exists = this.itemDetailsPerTab.some(existingItem => existingItem.itemId === data.itemId);
                      if (!exists) {
                          this.itemDetailsPerTab.push(data);
                          console.log("ItemDetailsPerTab", data);
                      }
                  },
                  error: (error) => {
                      console.error('Error fetching items', error);
                  }
              });
          });
      } else {
          if (event.length === 0) {
              this.itemDetailsPerTab = [];
          } else {
              const removedItems = this.itemDetailsPerTab.filter(item =>
                  !this.selectedItems.some(selected => selected.itemId === item.itemId)
              );

              removedItems.forEach(item => this.onItemUncheck(item));
              this.getItemsBySubIdItemId(this.selectedItems);
          }
        }
    } else {
        this.itemDetailsPerTab = [];
    }
  }

  save(form: NgForm) {
    // Prepare the Permissionaction data
    this.Permissionaction.permTypeFk = this.perId;
    this.Permissionaction.subId = this.selectedSubWearFrom?.subId || 0;
    this.Permissionaction.destinationSubId = this.selectedSubWearTo?.subId || null;
    this.Permissionaction.permCreatedat = new Date().toISOString();

    // Use itemDetailsPerTab to get the correct quantities
    this.Permissionaction.items = this.itemDetailsPerTab.map(item => ({
        itemId: item.itemId,
        quantity: item.quantity || 0 // Use the dynamic quantity input
    }));

    // Log to verify that each item has a correct quantity value
    console.log('PermissionAction data:', this.Permissionaction);

    // Call the service
    this.permService.permissionAction(this.Permissionaction).subscribe({
        next: (response) => {
            console.log('Order created:', response);
            this.toastr.success('Order created successfully!', 'Success');
            this.ref.close(); // Optionally close the dialog
        },
        error: (error) => {
            console.error('Error creating permission:', error);
            this.toastr.error('Error creating permission', 'Error');
        }
    });
}
}
