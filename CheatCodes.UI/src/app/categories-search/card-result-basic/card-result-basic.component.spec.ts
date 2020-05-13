import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CardResultBasicComponent} from './card-result-basic.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoriesService} from '../../categories-generation/categories/categories.service';
import {CategoriesSearchHttpService} from '../categories-search-http.service';
import {CategoryBasic} from '../model/category';

describe('CardResultBasicComponent',
  () => {
    let component: CardResultBasicComponent;
    let fixture: ComponentFixture<CardResultBasicComponent>;

    beforeEach(async(() => {

      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        declarations: [CardResultBasicComponent],
        providers: [{provide: CategoriesSearchHttpService}]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(CardResultBasicComponent);
    });

    it('should create',
      () => {
        fixture.componentInstance.card = new CategoryBasic();
        component = fixture.componentInstance;
        fixture.detectChanges();

        expect(component).toBeTruthy();
      });
  });
