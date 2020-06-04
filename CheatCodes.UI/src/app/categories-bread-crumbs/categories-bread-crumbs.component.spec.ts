import {async, ComponentFixture, TestBed} from '@angular/core/testing';
import {CategoriesBreadCrumbsComponent} from './categories-bread-crumbs.component';
import {HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';
import {RouterTestingModule} from '@angular/router/testing';
import {CategoryBreadCrumbBuilder, CategoryBuilder} from '../categories-generation/models/category';
import {GetRandom} from '../test-utils/GetRandom';

describe('CategoriesBreadCrumbsComponent', () => {
  let fixture: ComponentFixture<CategoriesBreadCrumbsComponent>;
  let httpTestingController: HttpTestingController;
  const categoryUrl = 'https://localhost:44326/api/categories';

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [CategoriesBreadCrumbsComponent],
    }).compileComponents();
    httpTestingController = TestBed.inject(HttpTestingController);
  }));

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should create', () => {
    fixture = TestBed.createComponent(CategoriesBreadCrumbsComponent);
    expect(fixture.componentInstance).toBeTruthy();
  });
  it('ngInit should call getCategoryBreadCrumbs', () => {
    fixture = TestBed.createComponent(CategoriesBreadCrumbsComponent);
    const category = CategoryBuilder.basic().setParentId(GetRandom.Number()).build();
    fixture.componentInstance.categoryId = category.id;
    fixture.detectChanges();

    fixture.componentInstance.ngOnChanges();

    const req = httpTestingController.expectOne(categoryUrl + '/GetBreadCrumbs/' + category.id);
    expect(req.request.method).toEqual('GET');
  });

  function mockHttpResponseReturningFakeReturnBreadCrumb(fakeReturnBreadCrumb: CategoryBreadCrumbBuilder) {
    fixture = TestBed.createComponent(CategoriesBreadCrumbsComponent);
    // usually we sent the parentId but here we mock the http response so doesnt matter
    const categoryId = GetRandom.Number();
    fixture.componentInstance.categoryId = categoryId;
    fixture.detectChanges();
    fixture.componentInstance.ngOnChanges();

    const req = httpTestingController.expectOne(categoryUrl + '/GetBreadCrumbs/' + categoryId);
    req.flush({result: fakeReturnBreadCrumb});
  }

  it('should populate breadCrumbsText(1)', () => {
    const fakeBreadCrumb = CategoryBreadCrumbBuilder.basic();
    mockHttpResponseReturningFakeReturnBreadCrumb(fakeBreadCrumb);

    expect(fakeBreadCrumb.name + ' > ').toEqual(fixture.componentInstance.breadCrumbsText);
  });
  it('should populate breadCrumbsText(2)', () => {
    const fakeChildBreadCrumb = CategoryBreadCrumbBuilder.basic();
    const fakeBreadCrumb = CategoryBreadCrumbBuilder.basic().setChild(fakeChildBreadCrumb);
    mockHttpResponseReturningFakeReturnBreadCrumb(fakeBreadCrumb);

    expect(fakeBreadCrumb.name + ' > ' + fakeChildBreadCrumb.name + ' > ').toEqual(fixture.componentInstance.breadCrumbsText);
  });

  it('should populate breadCrumbsText(3)', () => {
    const fakeGrandChildBreadCrumb = CategoryBreadCrumbBuilder.basic();
    const fakeChildBreadCrumb = CategoryBreadCrumbBuilder.basic().setChild(fakeGrandChildBreadCrumb);
    const fakeBreadCrumb = CategoryBreadCrumbBuilder.basic().setChild(fakeChildBreadCrumb);
    mockHttpResponseReturningFakeReturnBreadCrumb(fakeBreadCrumb);

    expect(fakeBreadCrumb.name + ' > ' + fakeChildBreadCrumb.name + ' > ' + fakeGrandChildBreadCrumb.name + ' > ')
      .toEqual(fixture.componentInstance.breadCrumbsText);
  });
});
