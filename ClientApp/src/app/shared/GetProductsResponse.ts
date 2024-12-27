
export interface GetProductsResponse {
  items: GetProductsEntry[];
  total: number;
  page: number;
  limit: number;
}

export interface GetProductsEntry {
  productId: string
  ean: string
  name: string
}
