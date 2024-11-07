import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { WearhouseService } from '../wearhouse.service';
import { Location } from '@angular/common';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { ISubWearhouse } from 'src/app/shared/models/subwearhouse';

@Component({
  selector: 'app-subwearhouse-details',
  templateUrl: './subwearhouse-details.component.html',
  styleUrls: ['./subwearhouse-details.component.scss']
})
export class SubwearhouseDetailsComponent implements OnInit {
  subwearhouse: IViewWearhouseItem | null = null;
  wearhouseForm!: FormGroup;

  constructor(
    private wearhouseService: WearhouseService, 
    private activeRoute: ActivatedRoute, 
    private fb: FormBuilder,
    private toastr: ToastrService,
    private location: Location
  ) {}

  ngOnInit() {
    this.initForm();
    this.loadSubwearhouseDetails();
  }

  initForm() {
    this.wearhouseForm = this.fb.group({
      subName: ['', Validators.required],
      subDescription: [''],
      subAddress: ['']
    });
  }

  loadSubwearhouseDetails() {
    const mainID = this.activeRoute.snapshot.paramMap.get('id');
    if (mainID) {
      const numericItemID = +mainID;
      this.wearhouseService.getSubWearhouseById(numericItemID).subscribe(
        (data: IViewWearhouseItem) => {
          this.subwearhouse = data;
          this.populateForm();
        },
        error => {
          console.error('Error fetching sub-warehouse details:', error);
          this.toastr.error('Error fetching sub-warehouse details', 'Error');
        }
      );
    }
  }

  populateForm() {
    if (this.subwearhouse) {
      this.wearhouseForm.patchValue({
        subName: this.subwearhouse.subNameEn,
        subDescription: this.subwearhouse.subDescriptionEn,
        subAddress: this.subwearhouse.subAddressEn
      });
    }
  }

  onSubmit() {
    if (this.wearhouseForm.valid) {
      const updatedSubWearhouse: ISubWearhouse = this.wearhouseForm.value;
      const id = this.subwearhouse?.subId;
      if (id) {
        this.wearhouseService.updateSubWearhouse(id, updatedSubWearhouse).subscribe(
          response => {
            this.toastr.success('Sub-Warehouse updated successfully', 'Success');
          },
          error => {
            console.error('Error updating sub-warehouse:', error);
            this.toastr.error('Error updating sub-warehouse', 'Error');
          }
        );
      }
    } else {
      this.toastr.error('Please fill out the form correctly', 'Error');
    }
  }

  goBack() {
    this.location.back(); // Navigate back to the previous page
  }
}
