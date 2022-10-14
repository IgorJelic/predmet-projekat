import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { UserWrapComponent } from './components/users/user-wrap/user-wrap.component';
import { UserLoginComponent } from './components/users/user-login/user-login.component';
import { UserRegisterComponent } from './components/users/user-register/user-register.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UserHomeComponent } from './components/users/user-home/user-home.component';
import { UserEditProfileComponent } from './components/users/user-edit-profile/user-edit-profile.component';
import { UserDetailsComponent } from './components/users/user-details/user-details.component';
import { UserService } from './shared/services/user.service';
import { OrderService } from './shared/services/order.service';
import { ProductService } from './shared/services/product.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { UserVerificationsComponent } from './components/users/user-verifications/user-verifications.component';
import { ProductsViewAllComponent } from './components/products/products-view-all/products-view-all.component';
import { ProductsAddNewComponent } from './components/products/products-add-new/products-add-new.component';
import { OrdersViewAllComponent } from './components/orders/orders-view-all/orders-view-all.component';
import { OrdersCreateOrderComponent } from './components/orders/orders-create-order/orders-create-order.component';
import { OrdersCurrentOrderComponent } from './components/orders/orders-current-order/orders-current-order.component';
import { OrdersMyOrdersComponent } from './components/orders/orders-my-orders/orders-my-orders.component';
import { OrdersAvailableComponent } from './components/orders/orders-available/orders-available.component';

@NgModule({
  declarations: [
    AppComponent,
    UserWrapComponent,
    UserLoginComponent,
    UserRegisterComponent,
    UserHomeComponent,
    UserEditProfileComponent,
    UserDetailsComponent,
    UserVerificationsComponent,
    ProductsViewAllComponent,
    ProductsAddNewComponent,
    OrdersViewAllComponent,
    OrdersCreateOrderComponent,
    OrdersCurrentOrderComponent,
    OrdersMyOrdersComponent,
    OrdersAvailableComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    UserService,
    OrderService,
    ProductService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
