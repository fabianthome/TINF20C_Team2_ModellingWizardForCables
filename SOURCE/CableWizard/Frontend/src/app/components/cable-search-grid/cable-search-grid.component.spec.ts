import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableSearchGridComponent } from './cable-search-grid.component';

describe('CableSearchGridComponent', () => {
  let component: CableSearchGridComponent;
  let fixture: ComponentFixture<CableSearchGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableSearchGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableSearchGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
