import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesSearchContainerComponent } from './categories-search-container.component';

describe('CategoriesSearchContainerComponent', () => {
  let component: CategoriesSearchContainerComponent;
  let fixture: ComponentFixture<CategoriesSearchContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesSearchContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesSearchContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
