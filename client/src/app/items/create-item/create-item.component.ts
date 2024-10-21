import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ItemsService } from '../items.service';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Category } from 'src/app/shared/models/category';
import { CategoryService } from 'src/app/category/category.service';
import { Unit } from 'src/app/shared/models/unit';
import { UnitService } from 'src/app/unit/unit.service';

@Component({
  selector: 'app-create-item',
  templateUrl: './create-item.component.html',
  styleUrls: ['./create-item.component.scss']
})
export class CreateItemComponent implements OnInit{
  ItemForm!: FormGroup;
  errors: string[] = [];
  categories: Category[] = [];
  units: Unit[] = []; 
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private itemService: ItemsService,
    private categoryService: CategoryService,
    private unitService:UnitService,
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef
  ) { }
  
  ngOnInit(): void {
    this.createItemForm();
    this.loadCategories();
    this.loadUnits();
  }

  createItemForm(){
    this.ItemForm = this.fb.group({
      itemId: [0],
      itemCode: ['', Validators.required],
      itemName:  ['', Validators.required],
      catFk:[0, Validators.required],
      uniteFk:[0, Validators.required],
      itemExperationdate:[null],
      itemCreatedat:[null],
      itemUpdatedat:[null]
    });
  }
  loadCategories() {
    this.categoryService.getCtegories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: (err) => console.error(err)
    });
  }
  loadUnits() {
    this.unitService.getUnits().subscribe({
      next: (response) => {
        this.units = response;
      },
      error: (err) => console.error(err)
    });
  }
  save(){
    if (this.ItemForm.invalid) {
      this.toastr.error('Please fill in all required fields.');
      return;
    }
    const currentDate = new Date();
    this.ItemForm.patchValue({
      itemCreatedat: currentDate,
      itemUpdatedat: currentDate
    });
    
    this.itemService.createItem(this.ItemForm.value).subscribe({
      next: () => {
        this.toastr.success('Item created successfully');
        this.ItemForm.reset();
        this.ref.close('confirmed');
      },
      error: (error) => {
        console.error('Error details:', error);
        this.errors = (error.error && error.error.errors) || ['An unexpected error occurred'];
      }
    });
  }
}
