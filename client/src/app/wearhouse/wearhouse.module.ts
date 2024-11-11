import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MainwearhouseComponent } from './mainwearhouse/mainwearhouse.component';
import { MatCardModule } from '@angular/material/card';
import { SubwearhouseComponent } from './subwearhouse/subwearhouse.component';
import { RouterModule } from '@angular/router';
import { MainwearhouseDetalisComponent } from './mainwearhouse-detalis/mainwearhouse-detalis.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { MainModalComponent } from './main-modal/main-modal.component';
import { DialogModule } from 'primeng/dialog';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { MatButtonModule } from '@angular/material/button';
import { SubwearhouseTreeComponent } from './subwearhouse-tree/subwearhouse-tree.component';
import { SubwearhouseDetailsComponent } from './subwearhouse-details/subwearhouse-details.component';
import { CreatesubModalComponent } from './createsub-modal/createsub-modal.component';
import { ButtonModule } from 'primeng/button';
import { AccordionModule } from 'primeng/accordion';
import { TreeModule } from 'primeng/tree';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TreeTableModule } from 'primeng/treetable';
import { TooltipModule } from 'primeng/tooltip';
import { SubItemsComponent } from './sub-items/sub-items.component';
import { TableModule } from 'primeng/table';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    MainwearhouseComponent,
    SubwearhouseComponent,
    MainwearhouseDetalisComponent,
    MainModalComponent,
    SubwearhouseTreeComponent,
    SubwearhouseDetailsComponent,
    CreatesubModalComponent,
    SubItemsComponent
  ],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    MatCardModule,
    RouterModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 3000, // Set the duration in milliseconds
      positionClass: 'toast-bottom-right', // Set the position
      preventDuplicates: false, // Prevent duplicate messages

    }),
    DialogModule,
    DynamicDialogModule,
    MatButtonModule,
    ButtonModule,
    AccordionModule,
    TreeModule,
    BrowserAnimationsModule,
    TreeTableModule,
    TooltipModule,
    TableModule,
    SharedModule
  ],
  exports:[MainwearhouseComponent,SubwearhouseComponent,MainModalComponent,CreatesubModalComponent
    ,SubwearhouseComponent,SubwearhouseDetailsComponent]
})
export class WearhouseModule { }
