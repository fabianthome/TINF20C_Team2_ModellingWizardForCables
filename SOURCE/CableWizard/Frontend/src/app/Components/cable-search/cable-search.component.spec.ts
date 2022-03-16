import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableSearchComponent } from './cable-search.component';

describe('CableSearchComponent', () => {
  let component: CableSearchComponent;
  let fixture: ComponentFixture<CableSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
