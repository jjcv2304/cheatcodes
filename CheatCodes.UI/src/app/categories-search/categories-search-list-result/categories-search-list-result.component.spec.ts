import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesSearchListResultComponent } from './categories-search-list-result.component';

describe('CategoriesSearchListResultComponent', () => {
  let component: CategoriesSearchListResultComponent;
  let fixture: ComponentFixture<CategoriesSearchListResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesSearchListResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesSearchListResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
