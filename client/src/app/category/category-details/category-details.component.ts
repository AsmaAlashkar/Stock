import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../category.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/shared/models/category';
import { Location } from '@angular/common';

@Component({
  selector: 'app-category-details',
  templateUrl: './category-details.component.html',
  styleUrls: ['./category-details.component.scss']
})
export class CategoryDetailsComponent implements OnInit{
  category: Category | null = null;
  categoryForm!: FormGroup;

  constructor(
    private categoryService: CategoryService, 
    private activeRoute: ActivatedRoute, 
    private fb: FormBuilder,
    private toastr: ToastrService,
    private location: Location
  ) {}

  ngOnInit() {
    this.initForm();
    this.loadCategoryDetails();
  }

  initForm() {
    this.categoryForm = this.fb.group({
      catNameAr: ['', Validators.required],
      catNameEn: ['', Validators.required],
      catDesAr: [''],
      catDesEn: ['']
    });
  }

  loadCategoryDetails() {
    const categoryId = this.activeRoute.snapshot.paramMap.get('id');
    if (categoryId) {
      const numericCategoryId = +categoryId;
      this.categoryService.getCtegoryById(numericCategoryId).subscribe(
        (data: Category) => {
          this.category = data;
          this.populateForm();
        },
        error => {
          console.error('Error fetching category details:', error);
          this.toastr.error('Error fetching category details', 'Error');
        }
      );
    }
  }

  populateForm() {
    if (this.category) {
      this.categoryForm.patchValue({
        catNameAr: this.category.catNameAr,
        catNameEn: this.category.catNameEn,
        catDesAr: this.category.catDesAr,
        catDesEn: this.category.catDesEn
      });
    }
  }

  onSubmit() {
    if (this.categoryForm.valid) {
      const updatedCategory: Category = this.categoryForm.value;
      const id = this.category?.catId;
      if (id) {
        this.categoryService.updateCategory(id, updatedCategory).subscribe(
          {
            next: response => {
              this.toastr.success('Category updated successfully', 'Success');
            },
            error: error => {
              console.error('Error updating category:', error);
              this.toastr.error('Error updating category', 'Error');
            }
          }
        );
      }
    } else {
      this.toastr.error('Please fill out the form correctly', 'Error');
    }
  }

  goBack() {
    this.location.back(); // Navigate back to the previous page
  }
}
