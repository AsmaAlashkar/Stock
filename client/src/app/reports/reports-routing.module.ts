import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GenralReportComponent } from './genral-report/genral-report.component';
import { MainReportComponent } from './main-report/main-report.component';
import { SecondReportComponent } from './second-report/second-report.component';

const routes: Routes = [
  { path: '', component: GenralReportComponent, children: [
      { path: 'main-report', component: MainReportComponent },
      { path: 'second-report', component: SecondReportComponent }
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
