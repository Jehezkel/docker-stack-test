import { Component, inject, OnInit } from '@angular/core';
import { ProductFormComponent } from '../product-form/product-form.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiClientService, CreateProductRequest, UpdateProductRequest } from '../../shared/api-client.service';
import { catchError, Observable } from 'rxjs';
import { GetProductResponse } from '../../shared/GetProductResponse';
import { AsyncPipe } from '@angular/common';
import { ToastrService } from '../../shared/toastr/toastr.service';

@Component({
  selector: 'app-product',
  imports: [ProductFormComponent, AsyncPipe],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent implements OnInit {
  router = inject(Router);
  toastrService = inject(ToastrService)
  onFormSubmit($event: CreateProductRequest | UpdateProductRequest) {
    var action: Observable<any>;
    if (this.productId !== null) {
      action = this.apiService.updateProduct(this.productId, $event as UpdateProductRequest);

    } else {
      action = this.apiService.createProduct($event as CreateProductRequest);
    }

    action.
      subscribe({
        next: _ => {
          this.router.navigate(["/products"]);
          this.toastrService.success("Product Added")
        },
        error: error => {
          this.toastrService.error(error)
        }

      })
  }

  productId: null | string = null

  product$ = new Observable<GetProductResponse | null>()
  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('productId')
    const mode = this.productId !== null ? 'edit' : 'new'
    if (mode === "new" || this.productId === null) {
      return
    }
    this.product$ = this.apiService.getProduct(this.productId)
  }
  route = inject(ActivatedRoute)
  apiService = inject(ApiClientService)
}
