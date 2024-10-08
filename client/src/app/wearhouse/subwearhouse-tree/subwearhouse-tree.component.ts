import { Component, Input, OnInit } from '@angular/core';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';
import { WearhouseService } from '../wearhouse.service';
import { TreeNode } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-subwearhouse-tree',
  templateUrl: './subwearhouse-tree.component.html',
  styleUrls: ['./subwearhouse-tree.component.scss']
})
export class SubwearhouseTreeComponent implements OnInit {
  @Input() subWearhouses: IViewWearhouseItem[] = [];
  subWearhousestree!: TreeNode[];
  cols!: any[];

  constructor(private wearhouseService: WearhouseService, private activeRoute: ActivatedRoute) {}

  ngOnInit() {
    this.loadSubWarehouses();
    this.cols = [
      { field: 'subName', header: 'Sub Warehouse Name' },
      { field: 'subAddress', header: 'Sub Address' },
      { field: 'subDescription', header: 'Description' },
      { field: 'subCreatedat', header: 'Created At' },
      { field: 'subUpdatedat', header: 'Updated At' },
      { field: '', header: 'Actions' }
    ];
  }

  loadSubWarehouses() {
    const mainID = this.activeRoute.snapshot.paramMap.get('id');
    if (mainID) {
      const numericItemID = +mainID;
      this.wearhouseService.getSubWearhouseByMainId(numericItemID).subscribe(data => {
        this.subWearhouses = data;
        this.subWearhousestree = this.buildTree(data);
      });
    }
  }

  buildTree(data: IViewWearhouseItem[]): TreeNode[] {
    const map: { [key: number]: TreeNode } = {};
    const roots: TreeNode[] = [];

    data.forEach(item => {
      const node: TreeNode = {
        data: {
          subName: item.subName,
          subAddress: item.subAddress,
          subDescription: item.subDescription,
          subCreatedat: item.subCreatedat,
          subUpdatedat: item.subUpdatedat,
          subId: item.subId
        },
        children: []
      };
      map[item.subId] = node;

      if (item.parentSubWearhouseId) {
        const parent = map[item.parentSubWearhouseId];
        if (parent) {
          if (!parent.children) {
            parent.children = [];
          }
          parent.children.push(node);
        }
      } else {
        roots.push(node);
      }
    });

    return roots;
  }
}
