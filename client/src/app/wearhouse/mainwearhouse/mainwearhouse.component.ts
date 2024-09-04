import { Component } from '@angular/core';
import { IMainWearhouse } from 'src/app/shared/models/wearhouse';
import { WearhouseService } from '../wearhouse.service';

@Component({
  selector: 'app-mainwearhouse',
  templateUrl: './mainwearhouse.component.html',
  styleUrls: ['./mainwearhouse.component.scss']
})
export class MainwearhouseComponent {
mhouses! : IMainWearhouse[];

constructor(private mainwearService : WearhouseService){}

ngOnInit(): void {
  this.mainwearService.getmainwearhouse().subscribe(
    (data: IMainWearhouse[]) => {
      this.mhouses = data;
    },error => {console.log(error)}
    
  );
}

}
