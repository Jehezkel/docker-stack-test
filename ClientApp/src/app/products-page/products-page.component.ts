import { Component, inject, OnInit } from '@angular/core';
import { ApiClientService } from '../shared/api-client.service';
import { Observable, map } from 'rxjs';
import { GetProductsEntry, GetProductsResponse } from '../shared/GetProductsResponse';
import { AsyncPipe, NgFor } from '@angular/common';

@Component({
  selector: 'app-products-page',
  imports: [AsyncPipe, NgFor],
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss'
})
export class ProductsPageComponent {

  productsResponse$: Observable<GetProductsResponse>;
  products$: Observable<GetProductsEntry[]>;
  apiClient = inject(ApiClientService);
  constructor() {
    this.productsResponse$ = this.apiClient.getProducts();
    this.products$ = this.productsResponse$.pipe(map(r => r.items))
  }

}
