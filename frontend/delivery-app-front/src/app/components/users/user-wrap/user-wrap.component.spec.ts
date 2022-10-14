import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserWrapComponent } from './user-wrap.component';

describe('UserWrapComponent', () => {
  let component: UserWrapComponent;
  let fixture: ComponentFixture<UserWrapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserWrapComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserWrapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
