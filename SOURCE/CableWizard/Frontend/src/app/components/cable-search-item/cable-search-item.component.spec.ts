import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableSearchItemComponent } from './cable-search-item.component';

describe('CableSearchItemComponent', () => {
  let component: CableSearchItemComponent;
  let fixture: ComponentFixture<CableSearchItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableSearchItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableSearchItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
