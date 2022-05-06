import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CableEditorTabsComponent } from './cable-editor-tabs.component';

describe('CableEditorTabsComponent', () => {
  let component: CableEditorTabsComponent;
  let fixture: ComponentFixture<CableEditorTabsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CableEditorTabsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CableEditorTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
