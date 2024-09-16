import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { WearhouseService } from '../wearhouse.service';

@Component({
  selector: 'app-subwearhouse',
  templateUrl: './subwearhouse.component.html',
  styleUrls: ['./subwearhouse.component.scss']
})
export class SubwearhouseComponent implements OnInit {
  subwearhouses: IViewWearhouseItem[] = [];
  structuredWearhouses: IViewWearhouseItem[] = []; // Structured version for hierarchical display

  constructor(private mainwearService: WearhouseService, private activeRoute: ActivatedRoute) {}

  ngOnInit() {
    this.loadsubwearhouses();
  }

  loadsubwearhouses() {
    const mainID = this.activeRoute.snapshot.paramMap.get('id');
    if (mainID) {
      const numericItemID = +mainID;
      this.mainwearService.getSubWearhouseByMainId(numericItemID).subscribe(
        products => {
          if (Array.isArray(products)) {
            this.subwearhouses = products;
            this.structuredWearhouses = this.buildHierarchy(this.subwearhouses);
          } else {
            console.error('Unexpected API response:', products);
          }
        },
        error => {
          console.error('API call error:', error);
        }
      );
    }
  }

  // Recursively build the hierarchy
  buildHierarchy(subWearhouses: IViewWearhouseItem[], parentId: any = null): IViewWearhouseItem[] {

    // Filter sub-warehouses where the parentSubWearhouseId matches the parentId
    const filteredWearhouses = subWearhouses.filter(wh => wh.parentSubWearhouseId === parentId);


    // Return an empty array if no items match the parentId
    if (filteredWearhouses.length === 0) {
        return [];
    }

    // Recursively build the hierarchy for each filtered item
    return filteredWearhouses.map(wh => {
        // Recursively find children
        const children = this.buildHierarchy(subWearhouses, wh.subId);


        // Return the warehouse with its children
        return {
            ...wh,
            children: children.length > 0 ? children : []  // Ensure children is an empty array if no children exist
        };
    });
}



}
