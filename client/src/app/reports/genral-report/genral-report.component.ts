import { Component } from '@angular/core';

@Component({
  selector: 'app-genral-report',
  templateUrl: './genral-report.component.html',
  styleUrls: ['./genral-report.component.scss']
})
export class GenralReportComponent {

  currentDate: string;

  constructor() {
    const today = new Date();
    this.currentDate = today.toLocaleDateString();
    // this.currentDate = today.toLocaleDateString('en-US', {
    //   weekday: 'long',
    //   year: 'numeric',
    //   month: 'long',
    //   day: 'numeric'
    // });
  }

}
