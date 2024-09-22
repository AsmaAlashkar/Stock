import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { WearhouseService } from '../wearhouse.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog'; // Import these
import { ISubWearhouse } from 'src/app/shared/models/subwearhouse';

@Component({
  selector: 'app-createsub-modal',
  templateUrl: './createsub-modal.component.html',
  styleUrls: ['./createsub-modal.component.scss']
})
export class CreatesubModalComponent implements OnInit {
  SubMainWearhouseForm!: FormGroup;
  errors: string[] = [];
  ParentsubWearhouses: ISubWearhouse[] = [];
 
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private mainwearService: WearhouseService,
    public config: DynamicDialogConfig,  // Inject DynamicDialogConfig
    public ref: DynamicDialogRef  // Inject DynamicDialogRef for closing the modal
  ) {}

  ngOnInit(): void {
    this.createMainWearForm();
    
    const mainFk = this.SubMainWearhouseForm.get('mainFk')?.value;
    console.log('mainFk value:', mainFk);  // This will output the value of mainFk
    
    this.loadSubWearhouses(mainFk);
  }

  createMainWearForm() {
    // Initialize the form and set the mainFk value from the data passed into the modal
    this.SubMainWearhouseForm = this.fb.group({
      subId: [0],
      mainFk: [this.config.data.mainId, Validators.required],  // Set mainFk with the passed mainId
      parentSubWearhouseId: [null],
      subName: ['', Validators.required],
      subDescription: [''],
      subAddress: [''],
      mainCreatedat: [null],
      mainUpdatedat: [null],
      delet: [false],
      showParentSubWearhouse: [false]  // Add a form control for the checkbox
    });
  }

  loadSubWearhouses(mainId: number) {
    this.mainwearService.getSubNamesAndParentIdsByMainFk(mainId).subscribe({
      next: (response) => {
        console.log(response); // Log the response to check its structure
        this.ParentsubWearhouses = response;
      },
      error: (err) => console.error(err)
    });
  }



  save() {
    if (this.SubMainWearhouseForm.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }

    this.mainwearService.createNewSubWearhouse(this.SubMainWearhouseForm.value).subscribe({
      next: () => {
        this.toastr.success('Sub Warehouse created successfully');
        this.SubMainWearhouseForm.reset(); // Clear the form after successful creation
        this.ref.close(); // Close the modal after saving
      },
      error: (error) => {
        console.error('Error details:', error);
        this.errors = (error.error && error.error.errors) || ['An unexpected error occurred'];
      }
    });
  }
}
