import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersMyOrdersComponent } from './orders-my-orders.component';

describe('OrdersMyOrdersComponent', () => {
  let component: OrdersMyOrdersComponent;
  let fixture: ComponentFixture<OrdersMyOrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersMyOrdersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersMyOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
