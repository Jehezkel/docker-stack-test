import { Component, inject, OnInit } from '@angular/core';
import { ApiClientService } from '../shared/api-client.service';
import { BehaviorSubject, Observable, filter, map, switchMap } from 'rxjs';
import { GetProductsEntry, GetProductsResponse } from '../shared/GetProductsResponse';
import { AsyncPipe, NgFor } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { ModalService } from '../shared/modal/modal.service';
import { ToastrService } from '../shared/toastr/toastr.service';
import { ButtonStyle } from '../shared/button-style/button-style.component';
import { TableConfig } from '../master-data-page/master-data-page.component';
import { TableComponent } from "../shared/table/table.component";

@Component({
  selector: 'app-products-page',
  imports: [AsyncPipe, NgFor, RouterLink, ButtonStyle, TableComponent],
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss'
})
export class ProductsPageComponent implements OnInit {
  modalService = inject(ModalService)
  toastrService = inject(ToastrService)
  router = inject(Router)
  modalVisible = false;
  apiClient = inject(ApiClientService);
  refreshCall$ = new BehaviorSubject<null>(null)
  productsResponse$: Observable<GetProductsResponse> =
    this.refreshCall$.pipe(switchMap(_ => this.apiClient.getProducts()))
  products$ = this.productsResponse$.pipe(map(r => r.items))

  onDelete(product: GetProductsEntry) {
    this.modalService.show(`Are you sure to delete product EAN ${product.ean}`, "Delete", "Cancel", "Delete confirmation")
      .pipe(filter(result => result === true),
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
  getPathForEdit = (product: GetProductsEntry) => `edit/${product.productId}`
  ngOnInit(): void {
    this.refreshCall$.next(null)
  }
  productTableConfig: TableConfig = {
    columns: [
      { header: "Product Id", field: "productId" },
      { header: "Ean", field: "ean" },
      { header: "Product Name", field: "name" },
    ],
    actions: [
      { label: "Edit", icon: "edit", pathFn: (row) => this.getPathForEdit(row) },
      { label: "Delete", icon: "delete", fn: (row) => this.onDelete(row) }
    ]
  }

}
