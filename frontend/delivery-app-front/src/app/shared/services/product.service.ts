import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Product } from '../models/product/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  loadProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(environment.serverURL + '/Products');
  }

  loadProduct(id: number): Observable<Product> {
    return this.http.get<Product>(environment.serverURL + `/Products/${id}`);
  }

  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(
      environment.serverURL + '/Products',
      product
    );
  }
}
