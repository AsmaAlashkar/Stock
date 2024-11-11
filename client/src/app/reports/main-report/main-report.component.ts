import { Component, OnInit } from '@angular/core';
import { Reports } from 'src/app/shared/models/reports';
import { ReportsService } from '../reports.service';

@Component({
  selector: 'app-main-report',
  templateUrl: './main-report.component.html',
  styleUrls: ['./main-report.component.scss']
})
export class MainReportComponent implements OnInit{

  reports!: Reports[];

  constructor(private repo: ReportsService) { }

  ngOnInit() {
    this.repo.getAllItemsQuantitiesInAllSubsReports().subscribe({
      next: (data) => {
        this.reports = data;
        console.log(data);
      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    });
  }
  printPage() {
    window.print();
  }
  
}
