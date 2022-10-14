import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Order } from '../models/orders/order.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient) {}

  allOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(environment.serverURL + '/Orders');
  }

  myOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(environment.serverURL + '/Orders/MyOrders');
  }

  currentOrder(): Observable<Order> {
    return this.http.get<Order>(environment.serverURL + '/Orders/CurrentOrder');
  }

  availableOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(environment.serverURL + '/Orders/Available');
  }

  takeOrder(orderId: number): Observable<number> {
    return this.http.get<number>(
      environment.serverURL + `/Orders/TakeOrder/${orderId}`
    );
  }

  createOrder(order: Order): Observable<Object> {
    return this.http.post<Object>(
      environment.serverURL + '/Orders/MyOrders',
      order
    );
  }
}
