import {TestBed} from '@angular/core/testing';
import {HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';
import {HttpClient} from '@angular/common/http';
import {CategoryFilter} from '../categories-generation/models/CategoryFilter';
import {CategoriesSearchHttpService} from './categories-search-http.service';

describe('CategoriesSearchHttpService', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let service;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CategoriesSearchHttpService]
    });

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
    service = TestBed.inject(CategoriesSearchHttpService);
  });

  it('should create', () => {
    expect(service).toBeTruthy();
  });


});
