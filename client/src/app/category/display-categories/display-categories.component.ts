import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/shared/models/category';
import { CategoryService } from '../category.service';
import { CreateCategoryComponent } from '../create-category/create-category.component';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-display-categories',
  templateUrl: './display-categories.component.html',
  styleUrls: ['./display-categories.component.scss']
})
export class DisplayCategoriesComponent implements OnInit {

  categories!: Category[];
  categoryTree!: Category[];

  constructor(
    private categoryService : CategoryService,
    private dialogService: DialogService

  ) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCtegories().subscribe({
      next: (data) => {
        this.categories = data;
        this.categoryTree = this.buildHierarchy(data);
      },
      error: (error) => {
        console.error('Error fetching categories', error);
      }
    });
  }

  // Recursively build the hierarchy for categories
buildHierarchy(categories: Category[], parentId: number | null = null): Category[] {
  // Filter categories where the parentCategoryId matches the parentId
  const filteredCategories = categories.filter(cat => cat.parentCategoryId === parentId);

  // Recursively build the hierarchy for each filtered category
  return filteredCategories.map(cat => {
    const children = this.buildHierarchy(categories, cat.catId);
    return {
      ...cat,
      children: children.length > 0 ? children : [] // Ensure children is an empty array if no children exist
    };
  });
}

openCreateCategoryModal() {
  const catId = this.categories[0]?.catId;
  this.dialogService.open(CreateCategoryComponent, {
    data: { catId: catId },
    header: 'Create New Category',
    width: '70%',
    contentStyle: { 'max-height': '80vh', overflow: 'auto' }
  });
}
}
