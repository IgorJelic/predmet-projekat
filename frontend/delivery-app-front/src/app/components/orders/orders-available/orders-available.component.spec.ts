import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersAvailableComponent } from './orders-available.component';

describe('OrdersAvailableComponent', () => {
  let component: OrdersAvailableComponent;
  let fixture: ComponentFixture<OrdersAvailableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersAvailableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersAvailableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
