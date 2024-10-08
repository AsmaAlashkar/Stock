import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/shared/models/category';
import { CategoryService } from '../category.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent implements OnInit {

  categoryForm!: FormGroup;
  errors: string[] = [];
  Parentcategory: Category[] = [];

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private categoryService: CategoryService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef
  ) { }

  ngOnInit(): void {    
    this.createCategoryForm();
    this.loadCategories();
  }
  getValidParentCategoriesCount() {
    return this.Parentcategory.filter(c=>c.catId).length;
  }

  createCategoryForm() {
    this.categoryForm = this.fb.group({
      catId: [0],
      parentCategoryId: [null],
      catNameAr:  ['', Validators.required],
      catNameEn:  ['', Validators.required],
      catDesAr:  [null],
      catDesEn:  [null],
      level: [0],
      showParentCategory: [false] 
    });
  }

  loadCategories() {
    this.categoryService.getCtegories().subscribe({
      next: (response) => {
        console.log(response); 
        this.Parentcategory = response;
      },
      error: (err) => console.error(err)
    });
  } 

  save(){
    if (this.categoryForm.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }
    this.categoryService.createCategory(this.categoryForm.value).subscribe({
      next: () => {
        this.toastr.success('Category created successfully');
        this.categoryForm.reset(); 
        this.ref.close('confirmed'); 
      },
      error: (error) => {
        console.error('Error details:', error);
        this.errors = (error.error && error.error.errors) || ['An unexpected error occurred'];
      }
    });
  }

}
