import { Component } from '@angular/core';

@Component({
  selector: 'app-genral-report',
  templateUrl: './genral-report.component.html',
  styleUrls: ['./genral-report.component.scss']
})
export class GenralReportComponent {

  currentDate: string;

  constructor() {
    // const today = new Date();
    // this.currentDate = today.toLocaleDateString();
  
    const today = new Date();
    const arabicFormatter = new Intl.NumberFormat('ar-EG');

    const day = arabicFormatter.format(today.getDate()).padStart(2, '٠');  // Arabic zero
    const month = arabicFormatter.format(today.getMonth() + 1).padStart(2, '٠');
    const year = arabicFormatter.format(today.getFullYear()).replace(/٬/g, '');

    // Construct the date string in dd-mm-yy format with Arabic numerals
    this.currentDate = `${day}-${month}-${year}`;
  }

}
