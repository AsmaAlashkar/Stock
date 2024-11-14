import { Component, OnInit } from '@angular/core';
import { ReportsService } from '../reports.service';
import { Reports } from 'src/app/shared/models/reports';
import { ActivatedRoute } from '@angular/router';
import { subWearhouseVM } from 'src/app/shared/models/subwearhouse';
import { WearhouseService } from 'src/app/wearhouse/wearhouse.service';

@Component({
  selector: 'app-second-report',
  templateUrl: './second-report.component.html',
  styleUrls: ['./second-report.component.scss']
})
export class SecondReportComponent implements OnInit{

  reports!: Reports[];

  subWearhouses: subWearhouseVM[] = [];
  selectedSubWear!: subWearhouseVM;

  constructor(private repo: ReportsService,
    private wearhouseService: WearhouseService
  ) { }

  ngOnInit() {
    this.wearhouseService.getsubWearhouseVM().subscribe({
      next: (data) => {
        this.subWearhouses = data.map(sub=>({
          subId: sub.subId, subNameEn: sub.subNameEn
        }));
        console.log("subWearhouses :", this.subWearhouses);
      },
      error: (error) => {
        console.error('Error fetching items', error);
      }
    });
  }

  onSubWearhouseChange() {
    if (this.selectedSubWear && this.selectedSubWear.subId) {
      let subId = this.selectedSubWear.subId;
      this.repo.getAllItemsQuantitiesBySubIdReports(subId).subscribe({
        next: (data) => {
          this.reports = data;
          console.log(data);
        },
        error: (error) => {
          console.error('Error fetching items', error);
          this.reports = [];
        }
      });
    } else {
      this.reports = [];
    }
  }

  printPage() {
    window.print();
  }
  
  formatArabicNumber(number: number): string {
    return new Intl.NumberFormat('ar-EG').format(number);
  }

}
