import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PermissionService } from '../permission.service';
import { Permissionaction } from 'src/app/shared/models/permissionaction';
import { WearhouseService } from 'src/app/wearhouse/wearhouse.service';
import { subWearhouseVM } from 'src/app/shared/models/subwearhouse';
import { skip } from 'rxjs';
import { ItemsService } from 'src/app/items/items.service';
import { ItemDetailsDtoVM } from 'src/app/shared/models/items';

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

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private permService: PermissionService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private wearhouseService: WearhouseService,
    private itemsService: ItemsService
  )
  {
    this.Permissionaction = {
      permId: 0, items: [{itemId: 0, destinationSubId: 0, quantity: 0, subId: 0}], permTypeFk: 0, permCreatedat: ""
    }
  }

  ngOnInit(): void {

    this.getItemsName();
    this.getSubwearhouse();

    this.headerValue = this.config.data.headerValue;
    // console.log(perId);
  }

  getItemsName() {
    this.itemsService.getItemsVM().subscribe({
      next: (data) => {
        this.ItemDetailsResultVM = data;

      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    });
  }

  getSubwearhouse() {
    this.wearhouseService.getsubWearhouse().subscribe({
      next: (data) => {
        this.subWearhouses = data;
        // this.subWearhouses = data.map(sub=>({
        //   subId:sub.subId,subName:sub.subName
        // }));
        console.log("subWearhouses :", this.subWearhouses);
      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    }
    );
  }

}
