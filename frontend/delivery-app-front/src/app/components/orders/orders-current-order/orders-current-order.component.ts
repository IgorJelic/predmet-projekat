import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/shared/models/orders/order.model';
import { TimerModel } from 'src/app/shared/models/orders/timer.model';
import { Product } from 'src/app/shared/models/product/product.model';
import { OrderService } from 'src/app/shared/services/order.service';
import { ProductService } from 'src/app/shared/services/product.service';

@Component({
  selector: 'app-orders-current-order',
  templateUrl: './orders-current-order.component.html',
  styleUrls: ['./orders-current-order.component.css'],
})
export class OrdersCurrentOrderComponent implements OnInit {
  products: Product[] = [];
  currentOrder: Order = new Order();
  userRole: string = '';

  interval: any;
  timer: TimerModel = new TimerModel();

  constructor(
    private orderService: OrderService,
    private productService: ProductService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
  }

  loadProducts(): void {
    this.productService.loadProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
        this.loadRole();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Error loading products!', error.message);
      }
    );
  }

  loadRole(): void {
    let tokenString = localStorage.getItem('token')!;
    let jwtData = tokenString.split('.')[1];
    let decodedJwtJsonData = window.atob(jwtData);
    let decodedJwtData = JSON.parse(decodedJwtJsonData);

    this.userRole = decodedJwtData.role;

    this.loadCurrentOrder();
  }

  loadCurrentOrder(): void {
    this.orderService.currentOrder().subscribe((data: Order) => {
      //? nema trenutne porudzbine
      if (data.id == -1) {
        this.toastr.info('Nema trenutne porudzbine.');
        if (this.userRole == 'customer') {
          this.router.navigate(['../create-order'], {
            relativeTo: this.route,
          });
        } else if (this.userRole == 'deliverer') {
          this.router.navigate(['../available-orders'], {
            relativeTo: this.route,
          });
        } else {
          this.router.navigate(['../profile'], {
            relativeTo: this.route,
          });
        }
      }
      //? ima trenutne porudzbine
      else {
        this.currentOrder = data;

        if (this.currentOrder.confirmed == false) {
          this.toastr.info('Porudzbina ceka potvrdu dostavljaca.');
        } else {
          this.setTimer(data.deliveryTime);
          this.interval = setInterval(() => {
            this.timer.seconds -= 1;
            if (this.delivered()) {
              this.toastr.success('DELIVERED!');
              clearInterval(this.interval);
              this.router.navigate(['']);
            }
            if (this.timer.seconds < 0) {
              this.timer.minutes -= 1;
              this.timer.seconds = 59;
            }
          }, 1000);
        }
      }
    });
  }

  setTimer(deliveryTime: Date): void {
    let nowDate = new Date(Date.now());

    let time = deliveryTime.toString().split('T')[1];
    let deliveryHours = Number.parseInt(time.split(':')[0]);
    let deliveryMinutes = Number.parseInt(time.split(':')[1]);
    let deliverySeconds = Number.parseInt(time.split(':')[2].split('.')[0]);

    let nowMinutes = nowDate.getMinutes();
    let nowSeconds = nowDate.getSeconds();

    if (deliveryHours > nowDate.getHours()) deliveryMinutes += 60;
    this.timer.minutes = deliveryMinutes - nowMinutes;
    if (deliverySeconds < nowSeconds) {
      this.timer.minutes -= 1;
      this.timer.seconds = 60 - (nowSeconds - deliverySeconds);
    } else this.timer.seconds = deliverySeconds - nowSeconds;
  }

  delivered(): boolean {
    if (this.timer.minutes == 0 && this.timer.seconds == 0) return true;
    else return false;
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
