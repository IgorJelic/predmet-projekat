import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersCurrentOrderComponent } from './orders-current-order.component';

describe('OrdersCurrentOrderComponent', () => {
  let component: OrdersCurrentOrderComponent;
  let fixture: ComponentFixture<OrdersCurrentOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersCurrentOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersCurrentOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
