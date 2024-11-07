import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { WearhouseService } from '../wearhouse.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-mainwearhouse-detalis',
  templateUrl: './mainwearhouse-detalis.component.html',
  styleUrls: ['./mainwearhouse-detalis.component.scss']
})
export class MainwearhouseDetalisComponent {
  mainwearhouse: IViewWearhouseItem[] = [];
  wearhouseForm!: FormGroup;

  constructor(
    private mainwearService: WearhouseService, 
    private activeRoute: ActivatedRoute, 
    private fb: FormBuilder  ,
    private toastr: ToastrService,
  ) {}

  ngOnInit() {
    this.initForm();  
    this.loadmainwearhouse();
  }

  initForm() {
    this.wearhouseForm = this.fb.group({
      mainName: [''],       
      mainDescription: [''],
      mainAdderess: [''],
      mainCreatedat: [''],
      mainUpdatedat: [''],
      
    });
  }

  loadmainwearhouse() {
    const mainID = this.activeRoute.snapshot.paramMap.get('id');
    if (mainID) {
      const numericItemID = +mainID;
      this.mainwearService.getmainwearhousebyid(numericItemID).subscribe(
        product => {
          this.mainwearhouse = product;
          this.populateForm();  
        },
        error => {
          console.log(error);
        }
      );
    }
  }

  populateForm() {
    if (this.mainwearhouse && this.mainwearhouse.length > 0) {
      const data = this.mainwearhouse[0];  
      this.wearhouseForm.patchValue({
        mainName: data.mainNameEn,
        mainDescription: data.mainDescriptionEn,
        mainAdderess: data.mainAdderess,
        mainCreatedat: data.mainCreatedat,
        mainUpdatedat: data.mainUpdatedat,
        
      });
    }
  }

  // Handle form submission
  onSubmit() {
    if (this.wearhouseForm.valid) {
      const updatedWearhouse = this.wearhouseForm.value;
      const mainID = this.activeRoute.snapshot.paramMap.get('id');
      if (mainID) {
        const numericItemID = +mainID;
        this.mainwearService.updateMainWearhouse(numericItemID, updatedWearhouse).subscribe(
          response => {
            this.toastr.success('Update Main WareHouse Successfully')
            // Handle success
          },
          error => {
            console.log('Update error:', error);
            // Handle error
          }
        );
      }
    }
  }
  


  getDayAndMonth(dateString: string): { dayOfWeek: string, monthName: string } {
    const daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    const date = new Date(dateString);
    const dayOfWeekIndex = date.getDay();
    const monthIndex = date.getMonth();
    return {
      dayOfWeek: daysOfWeek[dayOfWeekIndex],
      monthName: months[monthIndex]
    };
  }
}
