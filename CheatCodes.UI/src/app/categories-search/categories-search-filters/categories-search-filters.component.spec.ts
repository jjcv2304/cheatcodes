import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { CategoriesSearchFiltersComponent } from "./categories-search-filters.component";

describe("CategoriesSearchFiltersComponent",
  () => {
    let component: CategoriesSearchFiltersComponent;
    let fixture: ComponentFixture<CategoriesSearchFiltersComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
          declarations: [CategoriesSearchFiltersComponent]
        })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(CategoriesSearchFiltersComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it("should create",
      () => {
        expect(component).toBeTruthy();
      });
  });
