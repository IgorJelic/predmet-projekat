import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/shared/models/orders/order.model';
import { Product } from 'src/app/shared/models/product/product.model';
import { OrderService } from 'src/app/shared/services/order.service';
import { ProductService } from 'src/app/shared/services/product.service';

@Component({
  selector: 'app-orders-available',
  templateUrl: './orders-available.component.html',
  styleUrls: ['./orders-available.component.css'],
})
export class OrdersAvailableComponent implements OnInit {
  products: Product[] = [];
  availableOrders: Order[] = [];

  constructor(
    private productService: ProductService,
    private orderService: OrderService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // ako ne postoji currentOrder, moguce je dodati novu porudzbinu, u suprotnom
    // redirekcija na stranicu sa tajmerom
    this.checkCurrentOrder();

    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.loadProducts().subscribe(
      (data: Product[]) => {
        this.products = data;

        this.loadAvailableOrders();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Error loading products!', error.message);
      }
    );
  }

  loadAvailableOrders(): void {
    this.orderService.availableOrders().subscribe(
      (data: Order[]) => {
        this.availableOrders = data;
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Error loading available products!', error.message);
      }
    );
  }

  checkCurrentOrder(): boolean {
    let retVal: boolean;

    this.orderService.currentOrder().subscribe(
      (data: Order) => {
        if (data.id == -1) {
          this.toastr.info('Info', 'Nema trenutne porudzbine.');
          return false;
        } else
          this.router.navigate(['../current-order'], {
            relativeTo: this.route,
          });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(
          'Neuspesno ucitavanje trenutne porudzbine DELIVERER-a!',
          error.message
        );
        return false;
      }
    );

    return retVal!;
  }

  takeOrder(orderId: number): void {
    this.orderService.takeOrder(orderId).subscribe(
      (data: number) => {
        this.toastr.success(
          'Success!',
          `Uspesno preuzeta porudzbina ID = ${data}`
        );
        window.location.reload();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Error taking order!', error.message);
      }
    );
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
