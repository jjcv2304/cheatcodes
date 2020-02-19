import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesSearchResultsComponent } from './categories-search-results.component';

describe('CategoriesSearchResultsComponent', () => {
  let component: CategoriesSearchResultsComponent;
  let fixture: ComponentFixture<CategoriesSearchResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesSearchResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesSearchResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
