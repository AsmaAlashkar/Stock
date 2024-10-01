import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/shared/models/category';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-display-categories',
  templateUrl: './display-categories.component.html',
  styleUrls: ['./display-categories.component.scss']
})
export class DisplayCategoriesComponent implements OnInit{
  categories!: Category[];

  constructor(private categoryService : CategoryService) {}
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
