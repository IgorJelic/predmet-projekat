import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsAddNewComponent } from './products-add-new.component';

describe('ProductsAddNewComponent', () => {
  let component: ProductsAddNewComponent;
  let fixture: ComponentFixture<ProductsAddNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductsAddNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductsAddNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
