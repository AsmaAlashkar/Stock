import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { WearhouseService } from '../wearhouse.service';
import { Location } from '@angular/common';  // Import Location

@Component({
  selector: 'app-subwearhouse-details',
  templateUrl: './subwearhouse-details.component.html',
  styleUrls: ['./subwearhouse-details.component.scss']
})
export class SubwearhouseDetailsComponent implements OnInit {
  subwearhouse: IViewWearhouseItem[] = [];
  wearhouseForm!: FormGroup;
  mainId!: number;

  constructor(
    private wearhouseService: WearhouseService, 
    private activeRoute: ActivatedRoute, 
    private fb: FormBuilder,
    private toastr: ToastrService,
    private cd: ChangeDetectorRef,
    private location: Location  // Inject Location
  ) {}

  ngOnInit() {
    this.wearhouseForm = this.fb.group({
      subName: [''],
      subDescription: [''],
      subAddress: ['']
    });

    this.mainId = +this.activeRoute.snapshot.paramMap.get('id')!;
    this.loadSubwearhouseDetails();
  }

  loadSubwearhouseDetails() {
    this.wearhouseService.getSubWearhouseById(this.mainId).subscribe(
      (data: IViewWearhouseItem) => {
        this.subwearhouse = [data];
        this.wearhouseForm.patchValue({
          subName: data.subName,
          subDescription: data.subDescription,
          subAddress: data.subAddress
        });
        this.cd.detectChanges();
      },
      (error) => {
        this.toastr.error('Error fetching sub-warehouse details', 'Error');
      }
    );
  }

  goBack() {
    this.location.back();  // Navigate back to the previous page
  }
}
