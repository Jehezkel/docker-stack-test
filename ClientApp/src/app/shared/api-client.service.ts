import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { GetProductsResponse } from './GetProductsResponse';
import { GetProductResponse } from './GetProductResponse';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  editMasterDataRow(entityName: string, id: string, request: any): any {
    console.log(request)
    const url = `${this.webApiBase}/master-data/${entityName}/${id}`
    return this.httpClient.put(url, request)
  }
  deleteMasterDataRow(entityName: string, id: any): any {
    const url = `${this.webApiBase}/master-data/${entityName}/${id}`
    return this.httpClient.delete<any[]>(url)
  }
  getMasterDataRows(entityName: string) {

    const url = `${this.webApiBase}/master-data/${entityName}`
    return this.httpClient.get<any[]>(url)
  }
  updateProduct(productId: string, request: UpdateProductRequest) {
    const url = `${this.webApiBase}/products/${productId}`
    return this.httpClient.put(url, request)
  }
  webApiBase = environment.API_URL_BASE
  httpClient = inject(HttpClient)

  getWeather = () => this.httpClient.get(this.webApiBase + "/weather-forecast")

  getProducts() {
    const url = this.webApiBase + "/products"
    return this.httpClient.get<GetProductsResponse>(url)
  }

  getProduct(productId: string) {
    const url = `${this.webApiBase}/products/${productId}`
    return this.httpClient.get<GetProductResponse>(url)
  }

  deleteProduct(productId: string) {
    const url = `${this.webApiBase}/products/${productId}`
    return this.httpClient.delete<GetProductResponse>(url)
  }

  createProduct(request: CreateProductRequest) {
    const url = this.webApiBase + "/products"
    return this.httpClient.post<CreateProductResponse>(url, request)
  }
  createMasterDataRow(entityName: string, request: any) {
    const url = `${this.webApiBase}/master-data/${entityName}`
    return this.httpClient.post(url, request)
  }
}
export interface CreateProductRequest {
  name: string
  ean: string
}
export interface CreateProductResponse {
  productId: string
}
export interface UpdateProductRequest {
  name: string
}
