import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableSearchNumberRangeComponent } from './cable-search-number-range.component';

describe('CableSearchNumberRangeComponent', () => {
  let component: CableSearchNumberRangeComponent;
  let fixture: ComponentFixture<CableSearchNumberRangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableSearchNumberRangeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableSearchNumberRangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
