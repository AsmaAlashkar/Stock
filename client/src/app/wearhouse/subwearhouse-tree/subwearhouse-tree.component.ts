import { Component, Input } from '@angular/core';
import { IViewWearhouseItem } from 'src/app/shared/models/IViewWearhouseItem';

@Component({
  selector: 'app-subwearhouse-tree',
  templateUrl: './subwearhouse-tree.component.html',
  styleUrls: ['./subwearhouse-tree.component.scss']
})
export class SubwearhouseTreeComponent {
  @Input() subWearhouses: IViewWearhouseItem[] = [];


  
}
