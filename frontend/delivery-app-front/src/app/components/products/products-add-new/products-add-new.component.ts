import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/services/product.service';
import { Product } from 'src/app/shared/models/product/product.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-products-add-new',
  templateUrl: './products-add-new.component.html',
  styleUrls: ['./products-add-new.component.css'],
})
export class ProductsAddNewComponent implements OnInit {
  productForm = new FormGroup({
    name: new FormControl('', Validators.required),
    price: new FormControl('', [Validators.required, Validators.min(1)]),
    ingredients: new FormControl('', Validators.required),
  });

  constructor(
    private productService: ProductService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {}

  onSubmit(): void {
    if (!this.productForm.valid) {
      this.toastr.error('Form invalid!');
      return;
    }

    let product = new Product();
    product.name = this.productForm.controls.name.value!;
    product.price = Number.parseInt(this.productForm.controls.price.value!);
    product.ingredients = this.productForm.controls.ingredients.value!;

    this.productService.addProduct(product).subscribe(
      (data: Product) => {
        this.toastr.success('Uspesno dodat proizvod: ' + data.name);
        this.router.navigate(['../dodavanje-proizvoda'], {
          relativeTo: this.route,
        });
      },
      (error: HttpErrorResponse) => {
        this.toastr.error('Neuspesno dodavanje proizvoda!', error.message);
      }
    );
  }
}
