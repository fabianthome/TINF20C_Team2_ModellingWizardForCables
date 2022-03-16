import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableTemplatesGridComponent } from './cable-templates-grid.component';

describe('CableTemplatesGridComponent', () => {
  let component: CableTemplatesGridComponent;
  let fixture: ComponentFixture<CableTemplatesGridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableTemplatesGridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableTemplatesGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
