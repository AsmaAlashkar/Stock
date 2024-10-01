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
  categoryTree!: Category[];
  constructor(
    private categoryService : CategoryService,

  ) {}
  ngOnInit(): void {
    this.loadCategories();
  }
  loadCategories(){
    this.categoryService.getCtegories().subscribe({
      next:(data) => {        
        this.categories = data;
        this.categoryTree = this.buildTree(data);
      },
      error:(error) => {
        console.error('Error fetching categories', error);
      }
    });
  }
  buildTree(categories: Category[]): Category[] {
    const map = new Map<number, Category[]>();
    
    categories.forEach((category) => {
      const parentId = category.parentCategoryId || 0;
      if (!map.has(parentId)) {
        map.set(parentId, []);
      }
      map.get(parentId)?.push(category);
    });

    return this.buildBranch(map, null);
  }

  buildBranch(map: Map<number, Category[]>, parentId: number | null): Category[] {
    return map.get(parentId ?? 0)?.map((category) => {
      return {
        ...category,
        children: this.buildBranch(map, category.catId),
      };
    }) || [];
  }
}
