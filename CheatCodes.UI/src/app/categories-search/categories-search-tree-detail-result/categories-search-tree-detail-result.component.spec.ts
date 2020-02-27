import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesSearchTreeDetailResultComponent } from './categories-search-tree-detail-result.component';

describe('CategoriesSearchTreeDetailResultComponent', () => {
  let component: CategoriesSearchTreeDetailResultComponent;
  let fixture: ComponentFixture<CategoriesSearchTreeDetailResultComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesSearchTreeDetailResultComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesSearchTreeDetailResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
