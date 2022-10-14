import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersCreateOrderComponent } from './orders-create-order.component';

describe('OrdersCreateOrderComponent', () => {
  let component: OrdersCreateOrderComponent;
  let fixture: ComponentFixture<OrdersCreateOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersCreateOrderComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersCreateOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
