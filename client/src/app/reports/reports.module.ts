import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GenralReportComponent } from './genral-report/genral-report.component';
import { RouterModule } from '@angular/router';
import { MainReportComponent } from './main-report/main-report.component';
import { ReportsRoutingModule } from './reports-routing.module';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { SecondReportComponent } from './second-report/second-report.component';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    GenralReportComponent,
    MainReportComponent,
    SecondReportComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReportsRoutingModule,
    TableModule,
    ButtonModule,
    DropdownModule,
    FormsModule
  ]
})
export class ReportsModule { }
