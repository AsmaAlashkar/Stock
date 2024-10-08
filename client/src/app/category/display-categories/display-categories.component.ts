import { Component, OnInit } from '@angular/core';
import { Category, Column } from 'src/app/shared/models/category';
import { CategoryService } from '../category.service';
import { CreateCategoryComponent } from '../create-category/create-category.component';
import { DialogService } from 'primeng/dynamicdialog';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'app-display-categories',
  templateUrl: './display-categories.component.html',
  styleUrls: ['./display-categories.component.scss']
})
export class DisplayCategoriesComponent implements OnInit {

  categories!: Category[];
  categoryTree!: TreeNode[];
  cols!: Column[];
  constructor(
    private categoryService: CategoryService,
    private dialogService: DialogService

  ) { }

  ngOnInit(): void {
    this.loadCategories();

    this.cols = [
      { field: 'catNameAr', header: 'Name (Arabic)' },
      { field: 'catNameEn', header: 'Name (English)' },
      { field: 'catDesAr', header: 'Description (Arabic)' },
      { field: 'catDesEn', header: 'Description (English)' },
      { field: '', header: 'Actions' } 
    ];
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
  buildHierarchy(categories: Category[], parentId: number | null = null): TreeNode[] {
    const filteredCategories = categories.filter(cat => cat.parentCategoryId === parentId);

    return filteredCategories.map(cat => ({
      data: {
        catId: cat.catId,
        catNameAr: cat.catNameAr,
        catNameEn: cat.catNameEn,
        catDesAr: cat.catDesAr,
        catDesEn: cat.catDesEn,
      },
      children: this.buildHierarchy(categories, cat.catId)
    }));
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
