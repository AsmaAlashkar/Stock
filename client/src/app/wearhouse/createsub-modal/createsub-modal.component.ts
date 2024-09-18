import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { WearhouseService } from '../wearhouse.service';

@Component({
  selector: 'app-createsub-modal',
  templateUrl: './createsub-modal.component.html',
  styleUrls: ['./createsub-modal.component.scss']
})
export class CreatesubModalComponent implements OnInit {
  SubMainWearhouseForm!: FormGroup;
  errors: string[] = [];

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private mainwearService: WearhouseService
  ) {}

  ngOnInit(): void {
    this.createMainWearForm();
  }

  createMainWearForm() {
    this.SubMainWearhouseForm = this.fb.group({
      subId: [''],
      mainFk: ['', Validators.required],
      parentSubWearhouseId: [null],  // Ensure it's initialized as null if empty
      subName: ['', Validators.required],
      subDescription: [''],
      subAddress: [''],
      mainCreatedat: [null],
      mainUpdatedat: [null],
      delet: [false]
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
      },
      error: (error) => {
        console.error('Error details:', error);
        console.error('Error message:', error.message);
        console.error('Error body:', error.error); // Log the actual response from the server
        this.errors = (error.error && error.error.errors) || ['An unexpected error occurred'];
      }
    });
  }
  
}
