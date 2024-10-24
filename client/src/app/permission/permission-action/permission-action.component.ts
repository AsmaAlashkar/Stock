import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PermissionService } from '../permission.service';
import { Permissionaction } from 'src/app/shared/models/permissionaction';
import { WearhouseService } from 'src/app/wearhouse/wearhouse.service';
import { subWearhouseVM } from 'src/app/shared/models/subwearhouse';
import { ItemsService } from 'src/app/items/items.service';
import { ItemDetailsDtoVM } from 'src/app/shared/models/items';
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
  selectedSubWearhouse!: subWearhouseVM;
  filteredItems: ItemDetailsDtoVM[] = [];

  constructor(
    private fb: FormBuilder,
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

    this.getItemsName();
    this.getSubwearhouse();

    this.headerValue = this.config.data.headerValue;
    console.log(this.headerValue);
  }

  getSubwearhouse() {
    this.wearhouseService.getsubWearhouse().subscribe({
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
    // console.log("id sub", this.selectedSubWearhouse.subId);
    if (this.selectedSubWearhouse) {
      this.displayItemsBySubId(this.selectedSubWearhouse.subId);
    } else {
      this.filteredItems = [];
      this.selectedItems = [];
    }
  }

  displayItemsBySubId(subId: number) {
    console.log(" subId:",subId);
    if (this.headerValue === "اضافة" || this.headerValue === "إضافة" || this.headerValue === "أضافة") {
      this.itemsService.getItemsVM().subscribe({
        next: (data) => {
          this.ItemDetailsResultVM = data;
          // this.filteredItems = this.ItemDetailsResultVM;
          console.log("ItemDetailsResultVM", this.ItemDetailsResultVM);
        },
        error: (error) => {
          console.error('Error fetching items', error);
        }
      });
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

}
