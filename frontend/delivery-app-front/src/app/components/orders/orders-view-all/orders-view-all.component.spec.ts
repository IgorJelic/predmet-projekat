import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdersViewAllComponent } from './orders-view-all.component';

describe('OrdersViewAllComponent', () => {
  let component: OrdersViewAllComponent;
  let fixture: ComponentFixture<OrdersViewAllComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrdersViewAllComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OrdersViewAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
