import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { PermissionService } from '../permission.service';

@Component({
  selector: 'app-permission-action',
  templateUrl: './permission-action.component.html',
  styleUrls: ['./permission-action.component.scss']
})
export class PermissionActionComponent implements OnInit {

  permActForm!: FormGroup;
  errors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private permService: PermissionService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef
  ) { }

  ngOnInit(): void {
    this.permActionForm();

    const perId = this.config.data.perId;
    console.log(perId);
    // Set the perId in the form if it exists
    if (perId) {
      this.permActForm.patchValue({
        permTypeFk: perId // Set perId in the form
      });
    }
  }

  permActionForm() {
    this.permActForm = this.fb.group({
      permTypeFk: [null, Validators.required], // This is where the perId is set
      items: this.fb.array([ // Items array for additional data
        this.fb.group({
          itemId: [null, Validators.required],
          quantity: [null, Validators.required]
        })
      ])
    });
  }

  // Getter method to access the FormArray
  get items(): FormArray {
    return this.permActForm.get('items') as FormArray;
  }

  save() {
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
}