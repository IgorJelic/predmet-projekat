import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Product } from 'src/app/shared/models/product/product.model';
import { ProductService } from 'src/app/shared/services/product.service';

@Component({
  selector: 'app-products-view-all',
  templateUrl: './products-view-all.component.html',
  styleUrls: ['./products-view-all.component.css'],
})
export class ProductsViewAllComponent implements OnInit {
  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.loadProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(error.message);
      }
    );
  }
}
