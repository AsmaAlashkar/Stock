import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PermissionService } from '../permission.service';
import { Permissionaction } from 'src/app/shared/models/permissionaction';
import { WearhouseService } from 'src/app/wearhouse/wearhouse.service';
import { subWearhouseVM } from 'src/app/shared/models/subwearhouse';

@Component({
  selector: 'app-permission-action',
  templateUrl: './permission-action.component.html',
  styleUrls: ['./permission-action.component.scss']
})
export class PermissionActionComponent implements OnInit {

  permActForm!: FormGroup;
  errors: string[] = [];
  headerValue:string='';
  Permissionaction:Permissionaction;
  subWearhous: subWearhouseVM[] = [];

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private permService: PermissionService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef,
    private wearhouseService: WearhouseService
  )
  {
    this.Permissionaction={
      permId: 0, items: [{itemId: 0, destinationSubId: 0, quantity: 0, subId: 0}], permTypeFk: 0, permCreatedat: ""
    }
  }

  ngOnInit(): void {

    this.getSubwearhouse();
    this.permActionForm();

    this.headerValue = this.config.data.headerValue;
    // console.log(perId);
    // if (perValue) {
      console.log("value :",this.headerValue);

      // this.permActForm.patchValue({
      //   permTypeFk: perValue
      // });
    //}

  }

  getSubwearhouse() {

    this.wearhouseService.getsubWearhouse().subscribe({
      next: (data) => {
        this.subWearhous=data.map(sub=>({
          subId:sub.subId,subName:sub.subName
        }))
        console.log("res :", this.subWearhous);

      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    }
    );
  }

  permActionForm() {
    this.permActForm = this.fb.group({
      permTypeFk: [null, Validators.required],
      items: this.fb.array([
        this.fb.group({
          itemId: [null, Validators.required],
          subId: [null, Validators.required],
          destinationSubId: [null],
          quantity: [null, Validators.required]
        })
      ])
    });
  }

  get items(): FormArray {
    return this.permActForm.get('items') as FormArray;
  }

  addItem() {
    this.items.push(this.fb.group({
      itemId: [null, Validators.required],
      subId: [null, Validators.required],
      destinationSubId: [null],
      quantity: [null, Validators.required]
    }));
  }

  save() {
    console.log("this.permActForm :",this.permActForm.get("items"));

    if (this.permActForm.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }
    this.permService.permissionAction(this.permActForm.value).subscribe({
      next: () => {
        this.toastr.success('Permission created successfully');
        this.permActForm.reset();
        this.ref.close('confirmed');
      },
      error: (error) => {
        console.error('Error details:', error);
        this.errors = (error.error && error.error.errors) || ['An unexpected error occurred'];
      }
    });
  }

  // isDestinationSubIdVisible(): boolean {
  //   const permTypeFk = this.permActForm.get('permTypeFk')?.value;
  //   return permTypeFk !== perId;
  // }

}
