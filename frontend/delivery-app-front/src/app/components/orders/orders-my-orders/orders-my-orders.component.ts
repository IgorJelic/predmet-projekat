import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/shared/models/orders/order.model';
import { Product } from 'src/app/shared/models/product/product.model';
import { OrderService } from 'src/app/shared/services/order.service';
import { ProductService } from 'src/app/shared/services/product.service';

@Component({
  selector: 'app-orders-my-orders',
  templateUrl: './orders-my-orders.component.html',
  styleUrls: ['./orders-my-orders.component.css'],
})
export class OrdersMyOrdersComponent implements OnInit {
  products: Product[] = [];
  myOrders: Order[] = [];

  constructor(
    private orderService: OrderService,
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

        this.loadMyOrders();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Error loading products!', error.message);
      }
    );
  }

  loadMyOrders(): void {
    this.orderService.myOrders().subscribe((data: Order[]) => {
      this.myOrders = data;
    });
  }

  getProductById(id: number): Product {
    let retVal = new Product();

    for (let i = 0; i < this.products.length; i += 1) {
      if (this.products[i].id == id) {
        retVal = this.products[i];
        break;
      }
    }

    return retVal;
  }
}
