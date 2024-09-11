import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { WearhouseService } from '../wearhouse.service';

@Component({
  selector: 'app-subwearhouse',
  templateUrl: './subwearhouse.component.html',
  styleUrls: ['./subwearhouse.component.scss']
})
export class SubwearhouseComponent {
  subwearhouses: IViewWearhouseItem[] = [];

  constructor(private mainwearService : WearhouseService, 
    private activeRoute: ActivatedRoute) {}
  
  ngOnInit(){
   this.loadsubwearhouses();
  }
  loadsubwearhouses() {
    const mainID = this.activeRoute.snapshot.paramMap.get('id');  // Get 'id' inside the method
    if (mainID) {
      const numericItemID = +mainID;  // Convert the string to a number
      this.mainwearService.getSubWearhouseByMainId(numericItemID).subscribe(
        products => {
          this.subwearhouses = products; // Assign response to the array
        }, error => {
          console.log(error);
        }
      );
    }
  }
  
}

