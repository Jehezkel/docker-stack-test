import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-form',
  imports: [],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent {
  @Input() set productData(productData: any) {
    if (productData) {
      this.productForm.patchValue(productData)
    }
  }
  @Output() formSubmit = new EventEmitter<any>();
  productForm = new FormGroup({
    name: new FormControl("", [Validators.required]),
    ean: new FormControl("", [Validators.required])
  })
}
