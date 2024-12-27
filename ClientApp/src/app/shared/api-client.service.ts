import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { GetProductsResponse } from './GetProductsResponse';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  webApiBase = environment.API_URL_BASE
  httpClient = inject(HttpClient)

  getWeather = () => this.httpClient.get(this.webApiBase + "/weather-forecast")

  getProducts() {
    const url = this.webApiBase + "/products"
    return this.httpClient.get<GetProductsResponse>(url)
  }

  createProduct(request: CreateProductRequest) {
    const url = this.webApiBase + "/products"
    return this.httpClient.post<CreateProductResponse>(url, request)
  }
}
export interface CreateProductRequest {
  name: string
  ean: string
}
export interface CreateProductResponse {
  productId: string
}
