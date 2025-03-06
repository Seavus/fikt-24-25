import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DynamicPopupComponent } from './dynamic-popup.component';

describe('DynamicPopupComponent', () => {
  let component: DynamicPopupComponent;
  let fixture: ComponentFixture<DynamicPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DynamicPopupComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DynamicPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
