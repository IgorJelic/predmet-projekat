import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserVerificationsComponent } from './user-verifications.component';

describe('UserVerificationsComponent', () => {
  let component: UserVerificationsComponent;
  let fixture: ComponentFixture<UserVerificationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserVerificationsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserVerificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
