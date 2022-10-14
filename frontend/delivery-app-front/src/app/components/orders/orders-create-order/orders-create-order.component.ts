import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Order } from 'src/app/shared/models/orders/order.model';
import { OrderedProduct } from 'src/app/shared/models/orders/ordered.product.model';
import { Product } from 'src/app/shared/models/product/product.model';
import { OrderService } from 'src/app/shared/services/order.service';
import { ProductService } from 'src/app/shared/services/product.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-orders-create-order',
  templateUrl: './orders-create-order.component.html',
  styleUrls: ['./orders-create-order.component.css'],
})
export class OrdersCreateOrderComponent implements OnInit {
  products: Product[] = [];
  orderedProducts: OrderedProduct[] = [];
  order: Order = new Order();
  // orderPrice: number = 0;

  orderAddress: FormControl = new FormControl('', Validators.required);
  note: FormControl = new FormControl('', Validators.required);

  constructor(
    private orderService: OrderService,
    private productService: ProductService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // ako ne postoji currentOrder, moguce je dodati novu porudzbinu, u suprotnom
    // redirekcija na stranicu sa tajmerom
    this.checkCurrentOrder();

    this.loadProducts();
    this.order.price = 0 + environment.deliveryFee;
  }

  loadProducts(): void {
    this.productService.loadProducts().subscribe(
      (data: Product[]) => {
        this.products = data;
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(
          'Greska prilikom ucitavanja proizvoda!',
          error.message
        );
      }
    );
  }

  checkCurrentOrder(): void {
    let retVal: boolean;

    this.orderService.currentOrder().subscribe(
      (data: Order) => {
        if (data.id == -1) {
          this.toastr.info('Info', 'Nema trenutne porudzbine.');
          return;
        } else
          this.router.navigate(['../current-order'], {
            relativeTo: this.route,
          });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(
          'Neuspesno ucitavanje trenutne porudzbine CUSTOMER-a!',
          error.message
        );
        return;
      }
    );
  }

  addProduct(product: Product) {
    let newProduct = true;

    this.orderedProducts.forEach((op) => {
      if (op.productId == product.id) {
        op.amount += 1;
        newProduct = false;
      }
    });

    if (newProduct) {
      let newOrderedProduct = new OrderedProduct();
      newOrderedProduct.amount = 1;
      newOrderedProduct.productId = product.id;
      this.orderedProducts.push(newOrderedProduct);
    }

    this.order.price += product.price;
  }

  removeProduct(product: OrderedProduct) {
    let tempOrderedProducts = this.orderedProducts.filter((op) => {
      return op.productId !== product.productId;
    });

    this.orderedProducts = tempOrderedProducts;

    let removedProduct = this.getProductById(product.productId);
    this.order.price -= product.amount * removedProduct.price;

    this.toastr.info(
      'Removed',
      `Product ${removedProduct.name}, price: ${removedProduct.price}din.`
    );
  }

  makeOrder() {
    if (!this.orderFormValid()) {
      this.toastr.error('Nevalidna porudzbina!');
      return;
    }

    this.order.orderAddress = this.orderAddress.value!;
    this.order.note = this.note.value!;
    this.order.orderedProducts = this.orderedProducts;

    // console.warn('Ordered:\n' + JSON.stringify(this.order));

    this.orderService.createOrder(this.order).subscribe(
      (data) => {
        this.toastr.success('Porudzbina uspela!');
        window.location.reload();
      },
      (error: HttpErrorResponse) => {
        this.toastr.error(error.message);
      }
    );
  }

  orderFormValid(): boolean {
    if (
      this.note.invalid ||
      this.orderAddress.invalid ||
      this.orderedProducts.length == 0
    ) {
      return false;
    }
    return true;
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
