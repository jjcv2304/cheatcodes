import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesSummaryComponent } from './categories-summary.component';

describe('CategoriesSummaryComponent', () => {
  let component: CategoriesSummaryComponent;
  let fixture: ComponentFixture<CategoriesSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
