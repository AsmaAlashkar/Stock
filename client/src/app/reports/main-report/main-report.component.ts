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

  // printReportPDF(): void {
  //   let printWindow = window.open('', 'width=800,height=600');
  // let content = `
  //   <html>
  //     <head>
  //       <style>
  //         body {
  //           font-family: Arial, sans-serif;
  //           margin: 20px;
  //         }
  //         h1 {
  //           text-align: center;
  //           font-size: 24px;
  //         }
  //         .header {
  //           display: flex;
  //           align-items: center;
  //           justify-content: center;
  //           margin-bottom: 20px;
  //         }
  //         .logo {
  //           width: 110px;
  //           height: 80px;
  //           margin-right: 20px;
  //         }
  //         .text-center {
  //           text-align: center;
  //         }
  //         .grid {
  //           display: flex;
  //           justify-content: space-between;
  //         }
  //         table {
  //           width: 100%;
  //           border-collapse: collapse;
  //           margin-top: 20px;
  //         }
  //         th, td {
  //           border: 1px solid #ddd;
  //           padding: 8px;
  //           text-align: left;
  //         }
  //         th {
  //           background-color: #f2f2f2;
  //         }
  //       </style>
  //     </head>
  //     <body>

  //     <div class="header">
  //         <img src="http://localhost:4200/assets/images/logo.png" alt="logo" class="logo">
  //         <div class="grid text-center">
  //           <div class="g-col-6">شركة المستقبل للحلول التكنولوجية</div>
  //           <div class="g-col-6">تقرير باسماء المخازن الرئيسية و الفرعية</div>
  //         </div>
  //       </div>

  //       <h1>Report - Main and Sub Warehouses</h1>
  //       <table>
  //         <tr>
  //           <th>Name</th>
  //           <th>Quantity</th>
  //         </tr>
  //         ${this.reports.map(report => `
  //           <tr>
  //             <td>${report.itemName}</td>
  //             <td>${report.currentQuantity}</td>
  //           </tr>
  //         `).join('')}
  //       </table>
  //     </body>
  //   </html>
  // `;

  // printWindow?.document.write(content);
  // printWindow?.document.close();
  // printWindow?.focus();
  // printWindow?.print();
  // printWindow?.close();
  // }

  
}
