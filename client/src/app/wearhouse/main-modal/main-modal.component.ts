import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WearhouseService } from '../wearhouse.service';
import { DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-main-modal',
  templateUrl: './main-modal.component.html',
  styleUrls: ['./main-modal.component.scss']
})
export class MainModalComponent implements OnInit {
  MainWearhouseForm!: FormGroup;
  errors: string[] = [];

  constructor(private fb: FormBuilder,
              private router: Router,
              private toastr: ToastrService,
              private mainwearService: WearhouseService,
              private ref: DynamicDialogRef) { }

  ngOnInit(): void {
    this.createMainWearForm();
  }

  createMainWearForm() {
    this.MainWearhouseForm = this.fb.group({
      mainNameEn: ['',Validators.required],
      mainNameAr: ['',Validators.required],
      mainDescriptionEn: [''],
      mainDescriptionAr: [''],
      mainAdderess: [''],
      mainCreatedat: [null],
      mainUpdatedat: [null],
      delet: [false]
    });
  }

  save() {
    if (this.MainWearhouseForm.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }

    console.log('Form Values:', this.MainWearhouseForm.value); // Debugging line

    this.mainwearService.createNewMainWearhouse(this.MainWearhouseForm.value).subscribe({
      next: () => {
        this.toastr.success('Warehouse created successfully');
        this.ref.close("created")
        // this.router.navigateByUrl('/item');
      },
      error: (error) => {
        console.error('Error details:', error);
        console.error('Error message:', error.message);
        console.error('Error body:', error.error);
        this.errors = (error.error && error.error.errors) || ['An unexpected error occurred'];
      }
    });
  }
}
