import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesSearchCardComponent } from './categories-search-card.component';

describe('CategoriesSearchCardComponent', () => {
  let component: CategoriesSearchCardComponent;
  let fixture: ComponentFixture<CategoriesSearchCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoriesSearchCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoriesSearchCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
