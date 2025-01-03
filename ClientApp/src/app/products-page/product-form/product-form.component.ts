import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { InputComponent } from "../../shared/input/input.component";

@Component({
  selector: 'app-product-form',
  imports: [ReactiveFormsModule, RouterLink, InputComponent],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent {
  @Input() set productData(productData: any) {
    if (productData) {
      this.productForm.patchValue(productData)
      this.isEditMode = true
    }
  }
  @Output() formSubmit = new EventEmitter<any>();
  isEditMode = false

  productForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    ean: new FormControl("", [Validators.required])
  })

  onSubmit() {
    if (this.productForm.valid) {
      this.formSubmit.emit(this.productForm.value)
    }
  }
}
