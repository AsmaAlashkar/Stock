import { Component, Input, OnInit } from '@angular/core';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { WearhouseService } from '../wearhouse.service';
import { Column, ISubWearhouse } from 'src/app/shared/models/subwearhouse';
import { TreeNode } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-subwearhouse-tree',
  templateUrl: './subwearhouse-tree.component.html',
  styleUrls: ['./subwearhouse-tree.component.scss']
})
export class SubwearhouseTreeComponent implements OnInit{
  @Input() subWearhouses: IViewWearhouseItem[] = [];
  @Input() mainId!: number;
  subWearHouses!: ISubWearhouse[];
  subWearhousestree!: TreeNode[];
  cols!: Column[];

  constructor(private wearhouseService: WearhouseService,private activeRoute: ActivatedRoute) {}

  ngOnInit() {
    this.wearhouseService.getSubWearhouseByMainId(this.mainId).subscribe((subWearhouses) => {
      this.subWearhousestree = this.buildTreeNodes(subWearHouses, this.mainId);
    });

    this.cols = [
      { field: 'subName', header: 'Name' },
      { field: 'subDescription', header: 'Description' },
      { field: 'subAddress', header: 'Address' },
      { field: 'subCreatedat', header: 'Created At' },
      { field: 'subUpdatedat', header: 'Updated At' },
    ];
  }

  buildTreeNodes(items: ISubWearhouse[],mainId:number): TreeNode[] {

    const map: { [key: number]: TreeNode } = {};
    const roots: TreeNode[] = [];

    items.forEach(item => {
      map[item.subId] = { data: item, children: [] };
    });

    items.forEach(item => {
      if (item.parentSubWearhouseId) {
        map[item.parentSubWearhouseId].children!.push(map[item.subId]);
      } else {
        roots.push(map[item.subId]);
      }
    });

     return roots;
  }


    // ngOnInit() {
  //   this.loadSubWarehouses();
  // }

  // loadSubWarehouses() {
  //   const mainID = this.activeRoute.snapshot.paramMap.get('id');
  //   if (mainID) {
  //     const numericItemID = +mainID;
  //   this.wearhouseService.getSubWearhouseByMainId(numericItemID).subscribe(data => {
  //     this.subWearhouses = data;
  //     this.subWearhousestree = this.buildTree(data);
  //   });
  // }}

  // buildTree(data: IViewWearhouseItem[]): TreeNode[] {
  //   const map: { [key: number]: TreeNode } = {};
  //   const roots: TreeNode[] = [];

  //   data.forEach(item => {
  //     const node: TreeNode = {
  //       data: item,
  //       children: [],
  //     };
  //     map[item.subId] = node;

  //     if (item.parentSubWearhouseId) {
  //       const parent = map[item.parentSubWearhouseId];
  //       if (parent) {
  //         if (!parent.children) {
  //           parent.children = [];
  //         }
  //         parent.children.push(node);
  //       }
  //     } else {
  //       roots.push(node);
  //     }
  //   });

  //   return roots;
  // }
}
