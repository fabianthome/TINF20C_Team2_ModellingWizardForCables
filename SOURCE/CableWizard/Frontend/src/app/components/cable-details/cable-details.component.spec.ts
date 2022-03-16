import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableDetailsComponent } from './cable-details.component';

describe('CableDetailsComponent', () => {
  let component: CableDetailsComponent;
  let fixture: ComponentFixture<CableDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
