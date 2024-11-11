import { Component, OnInit } from '@angular/core';
import { ReportsService } from '../reports.service';
import { Reports } from 'src/app/shared/models/reports';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-second-report',
  templateUrl: './second-report.component.html',
  styleUrls: ['./second-report.component.scss']
})
export class SecondReportComponent implements OnInit{

  reports!: Reports[];

  constructor(private repo: ReportsService,
    private activeRoute: ActivatedRoute,
  ) { }

  ngOnInit() {
    const subId = this.activeRoute.snapshot.paramMap.get('subId');
    if (subId) {
      const numItemId = +subId;
      this.repo.getAllItemsQuantitiesBySubIdReports(numItemId).subscribe({
        next: (data) => {
          this.reports = data;
          console.log(data);
        },
        error: (error) => {
          console.error('Error fetching items', error);
        }
      });
    }
  }

}
