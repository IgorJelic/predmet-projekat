import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminGuard } from './auth/guards/admin.guard';
import { CustomerDelivererGuard } from './auth/guards/customer-deliverer.guard';
import { CustomerGuard } from './auth/guards/customer.guard';
import { DelivererGuard } from './auth/guards/deliverer.guard';
import { LoggedGuard } from './auth/guards/logged.guard';
import { OrdersAvailableComponent } from './components/orders/orders-available/orders-available.component';
import { OrdersCreateOrderComponent } from './components/orders/orders-create-order/orders-create-order.component';
import { OrdersCurrentOrderComponent } from './components/orders/orders-current-order/orders-current-order.component';
import { OrdersMyOrdersComponent } from './components/orders/orders-my-orders/orders-my-orders.component';
import { OrdersViewAllComponent } from './components/orders/orders-view-all/orders-view-all.component';
import { ProductsAddNewComponent } from './components/products/products-add-new/products-add-new.component';
import { ProductsViewAllComponent } from './components/products/products-view-all/products-view-all.component';
import { UserDetailsComponent } from './components/users/user-details/user-details.component';
import { UserEditProfileComponent } from './components/users/user-edit-profile/user-edit-profile.component';
import { UserHomeComponent } from './components/users/user-home/user-home.component';
import { UserLoginComponent } from './components/users/user-login/user-login.component';
import { UserRegisterComponent } from './components/users/user-register/user-register.component';
import { UserVerificationsComponent } from './components/users/user-verifications/user-verifications.component';
import { UserWrapComponent } from './components/users/user-wrap/user-wrap.component';

const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full' },
  {
    path: 'user',
    component: UserWrapComponent,
    children: [
      { path: 'registration', component: UserRegisterComponent },
      { path: 'login', component: UserLoginComponent },
    ],
  },
  {
    path: 'home',
    component: UserHomeComponent,
    canActivate: [LoggedGuard],

    children: [
      {
        path: '',
        redirectTo: 'profile',
        pathMatch: 'full',
      },
      {
        path: 'profile',
        component: UserDetailsComponent,
      },
      {
        path: 'edit-profile',
        component: UserEditProfileComponent,
      },
      //! Administrator
      {
        path: 'verifications',
        component: UserVerificationsComponent,
        canActivate: [AdminGuard],
      },
      {
        path: 'all-products',
        component: ProductsViewAllComponent,
        canActivate: [AdminGuard],
      },
      {
        path: 'new-product',
        component: ProductsAddNewComponent,
        canActivate: [AdminGuard],
      },
      {
        path: 'all-orders',
        component: OrdersViewAllComponent,
        canActivate: [AdminGuard],
      },
      //! Deliverer
      {
        path: 'available-orders',
        component: OrdersAvailableComponent,
        canActivate: [DelivererGuard],
      },

      //! Customer
      {
        path: 'create-order',
        component: OrdersCreateOrderComponent,
        canActivate: [CustomerGuard],
      },
      //! Customer AND Deliverer
      {
        path: 'current-order',
        component: OrdersCurrentOrderComponent,
        canActivate: [CustomerDelivererGuard],
      },
      {
        path: 'my-orders',
        component: OrdersMyOrdersComponent,
        canActivate: [CustomerDelivererGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
