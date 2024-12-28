import { Component, inject, OnInit } from '@angular/core';
import { ApiClientService } from '../shared/api-client.service';
import { BehaviorSubject, Observable, Subject, filter, map, switchMap } from 'rxjs';
import { GetProductsEntry, GetProductsResponse } from '../shared/GetProductsResponse';
import { AsyncPipe, NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ModalService } from '../shared/modal/modal.service';
import { ToastrService } from '../shared/toastr/toastr.service';

@Component({
  selector: 'app-products-page',
  imports: [AsyncPipe, NgFor, RouterLink],
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss'
})
export class ProductsPageComponent implements OnInit {
  modalService = inject(ModalService)
  toastrService = inject(ToastrService)
  onDelete(product: GetProductsEntry) {
    this.modalService.show(`Are you sure to delete product EAN ${product.ean}`, "Delete", "Cancel")
      .pipe(filter(result => result),
        switchMap(_ => this.apiClient.deleteProduct(product.productId)))
      .subscribe(
        {
          next:
            _ => {
              this.toastrService.success("Product deleted");
              this.refreshCall$.next(null);
            },
          error:
            _ => {
              this.toastrService.error("Error occured on product deletion");
              this.refreshCall$.next(null);
            }
        }
      )
  }

  modalVisible = false;
  productsResponse$: Observable<GetProductsResponse>;
  products$: Observable<GetProductsEntry[]>;
  apiClient = inject(ApiClientService);
  refreshCall$ = new BehaviorSubject<null>(null)
  constructor() {
    this.productsResponse$ = this.refreshCall$.pipe(switchMap(_ => this.apiClient.getProducts()))
    this.products$ = this.productsResponse$.pipe(map(r => r.items))
  }
  ngOnInit(): void {
    this.refreshCall$.next(null)
  }

}
