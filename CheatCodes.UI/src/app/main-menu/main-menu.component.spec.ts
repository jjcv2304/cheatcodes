import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {MainMenuComponent} from './main-menu.component';
import {AuthService} from '../security/auth-service.component';
import {HttpClientTestingModule} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';

describe('MainMenuComponent',
  () => {
    let component: MainMenuComponent;
    let fixture: ComponentFixture<MainMenuComponent>;

    beforeEach(async(() => {
      TestBed.configureTestingModule({
        imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
        declarations: [MainMenuComponent],
        providers: [{provide: AuthService}]
      })
        .compileComponents();
    }));

    beforeEach(() => {
      fixture = TestBed.createComponent(MainMenuComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('should create',
      () => {
        expect(component).toBeTruthy();
      });
  });
