import {TestBed, inject, fakeAsync, flush, async} from '@angular/core/testing';

import {CategoriesService} from './categories.service';
import {HttpClientTestingModule, HttpTestingController, RequestMatch} from '@angular/common/http/testing';
import {CategoryFilter} from '../models/CategoryFilter';
import {GetRandom} from '../../test-utils/GetRandom';
import {Category, CategoryBuilder} from '../models/category';
import {HttpClient} from '@angular/common/http';
import {of} from 'rxjs';
import {NewFieldBuilder} from '../models/NewField';
import {CategoryFieldValueBuilder} from '../models/categoryFieldValue';

describe('CategoriesService', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let service;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CategoriesService]
    });
    // httpTestingController = TestBed.get(HttpTestingController);
    // service = TestBed.get(CategoriesService);

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
    service = TestBed.inject(CategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('.SetCategoryFilter should set CurrentCategoryFilter', () => {
    service.SetCategoryFilter(new CategoryFilter());

    expect(service.GetCategoryFilter).toBeTruthy();
  });
  it('.SetCategoryFilter should call getAll', () => {
    service.SetCategoryFilter(new CategoryFilter());

    const req = httpTestingController.expectOne(service.categoryUrl);
    httpTestingController.verify();
  });
  it('.SetCategoryFilter byParent should set CategoryFilter.byParent = true ', () => {
    service.SetCategoryFilter(CategoryFilter.FilterByParent(1));

    expect(service.GetCategoryFilter().byParent).toEqual(true);
  });
  it('.SetCategoryFilter byParent should call GetChildsOf', () => {
    const categoryId: number = GetRandom.Number();
    service.SetCategoryFilter(CategoryFilter.FilterByParent(categoryId));

    const req = httpTestingController.expectOne(service.categoryUrl + '/GetChildsOf/' + categoryId);
    httpTestingController.verify();
  });
  it('.SetCategoryFilter byChild should set CategoryFilter.byChild = true ', () => {
    service.SetCategoryFilter(CategoryFilter.FilterByChild(1));

    expect(service.GetCategoryFilter().byChild).toEqual(true);
  });
  it('.SetCategoryFilter byChild should call GetSiblingsOf', () => {
    const categoryId: number = GetRandom.Number();
    service.SetCategoryFilter(CategoryFilter.FilterByChild(categoryId));

    const req = httpTestingController.expectOne(service.categoryUrl + '/GetSiblingsOf/' + categoryId);
    httpTestingController.verify();
  });
  it('.addCategory should call categoryUrl', () => {
    const addCategory = new CategoryBuilder().simple().build();

    service.addCategory(addCategory).subscribe();

    httpTestingController.expectOne(service.categoryUrl);
    httpTestingController.verify();
  });
  it('.addCategory should call post', () => {
    const addCategory = new CategoryBuilder().simple().build();

    service.addCategory(addCategory).subscribe();

    const req = httpTestingController.match(service.categoryUrl);
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('POST');
    httpTestingController.verify();
  });
  it('.addField should call categoryUrl', () => {
    const addField = new NewFieldBuilder().simple().build();

    service.addField(addField).subscribe();

    httpTestingController.expectOne(service.categoryUrl + '/AddField');
    httpTestingController.verify();
  });
  it('.addField should call post', () => {
    const addField = new NewFieldBuilder().simple().build();

    service.addField(addField).subscribe();

    const req = httpTestingController.match(service.categoryUrl + '/AddField');
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('PUT');
    httpTestingController.verify();
  });
  it('.updateCategory should call categoryUrl', () => {
    const updateCategory = new CategoryBuilder().simple().build();

    service.updateCategory(updateCategory).subscribe();

    httpTestingController.expectOne(service.categoryUrl);
    httpTestingController.verify();
  });
  it('.updateCategory should call put', () => {
    const updateCategory = new CategoryBuilder().simple().build();

    service.updateCategory(updateCategory).subscribe();

    const req = httpTestingController.match(service.categoryUrl);
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('PUT');
    httpTestingController.verify();
  });
  it('.moveCategoryUp should call categoryUrl/MoveUp', () => {
    const updateCategory = new CategoryBuilder().simple().build();

    service.moveCategoryUp(updateCategory).subscribe();

    httpTestingController.expectOne(service.categoryUrl + '/MoveUp');
    httpTestingController.verify();
  });
  it('.moveCategoryUp should call put', () => {
    const updateCategory = new CategoryBuilder().simple().build();

    service.moveCategoryUp(updateCategory).subscribe();

    const req = httpTestingController.match(service.categoryUrl + '/MoveUp');
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('PUT');
    httpTestingController.verify();
  });
  it('.moveCategoryToSibling should call categoryUrl/MoveToSibling', () => {
    const cardId = GetRandom.Number();
    const siblingId = GetRandom.Number();

    service.moveCategoryToSibling(cardId, siblingId).subscribe();

    httpTestingController.expectOne(service.categoryUrl + '/MoveToSibling');
    httpTestingController.verify();
  });
  it('.moveCategoryToSibling should call put', () => {
    const cardId = GetRandom.Number();
    const siblingId = GetRandom.Number();

    service.moveCategoryToSibling(cardId, siblingId).subscribe();

    const req = httpTestingController.match(service.categoryUrl + '/MoveToSibling');
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('PUT');
    httpTestingController.verify();
  });
  it('.updateCategoryField should call UpdateField', () => {
    const categoryFieldValue = new CategoryFieldValueBuilder().simple().build();

    service.updateCategoryField(categoryFieldValue);

    httpTestingController.expectOne(service.categoryUrl + '/UpdateField');
    httpTestingController.verify();
  });
  it('.updateCategoryField should call post', () => {
    const categoryFieldValue = new CategoryFieldValueBuilder().simple().build();

    service.updateCategoryField(categoryFieldValue);

    const req = httpTestingController.match(service.categoryUrl + '/UpdateField');
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('PUT');
    httpTestingController.verify();
  });
  it('.deleteCategory should call categoryUrl', () => {
    const addCategory = new CategoryBuilder().simple().build();

    service.deleteCategory(addCategory).subscribe();

    httpTestingController.expectOne(service.categoryUrl);
    httpTestingController.verify();
  });
  it('.deleteCategory should call delete', () => {
    const addCategory = new CategoryBuilder().simple().build();

    service.deleteCategory(addCategory).subscribe();

    const req = httpTestingController.match(service.categoryUrl);
    expect(req.length).toEqual(1);
    expect(req[0].request.method).toEqual('DELETE');
    httpTestingController.verify();
  });


  // describe('#getUsers', () => {
  //   it('should return an Observable<User[]>', () => {
  //     const dummyUsers = [
  //       { login: 'John' },
  //       { login: 'Doe' }
  //     ];
  //
  //     service.getUsers().subscribe(users => {
  //       expect(users.length).toBe(2);
  //       expect(users).toEqual(dummyUsers);
  //     });
  //
  //     const req = httpMock.expectOne(`${service.API_URL}/users`);
  //     expect(req.request.method).toBe("GET");
  //     req.flush(dummyUsers);
  //   });
  // });

});
