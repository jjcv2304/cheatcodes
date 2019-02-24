using System.Linq;
using Application.Categories.Queries;
using Application.Interfaces;
using Application.Interfaces.Tests;
using Domain;
using Moq;
using NUnit.Framework;

namespace Application.Categories.Tests
{
    [TestFixture]
    public class CategoryQueryTest
    {
        #region properties

        private CategoryQuery _categoryQueryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private InMemoryCategoryRepository _categoryRepositoryInMemory;
        private CategoryQuery _categoryQueryInMemory;

        #endregion

        #region Tests

        [SetUp]
        public void TestSetup()
        {
//            _categoryRepositoryMock = new Mock<ICategoryRepository>();
//            _categoryQueryMock = new CategoryQuery(_categoryRepositoryMock.Object);
//
//            _categoryRepositoryInMemory = new InMemoryCategoryRepository();
//            _categoryQueryInMemory = new CategoryQuery(_categoryRepositoryInMemory);
        }

        [Test]
        public void CategoryQuery_ReturnsVM()
        {
//            const long testId = 2;
//            var category = CategoryBuilderTest.BuildOne(testId);
//            _categoryRepositoryMock.Setup(x => x.GetById(testId)).Returns(category);
//
//            var categoryVM = _categoryQueryMock.ById(testId);
//
//            Assert.IsInstanceOf<CategoryVM>(categoryVM);
        }

        [Test]
        public void CategoryQuery_ById_ReturnsChildCategories()
        {
//            var parentCategory1 = new Category() {Id = 1, Name = "parentCategory1"};
//            var childCategory1 = new Category() {Id = 2, Name = "childCategory1"};
//            var childCategory2 = new Category() {Id = 3, Name = "childCategory2"};
//            parentCategory1.ChildCategories.Add(childCategory1);
//            parentCategory1.ChildCategories.Add(childCategory2);
//            _categoryRepositoryInMemory.Add(parentCategory1);
//
//            var categoryMatched = _categoryQueryInMemory.ById(1);
//
//            Assert.AreEqual(categoryMatched.ChildCategories.Count, 2);
        }

        [Test]
        public void CategoryQuery_ByExactName_ReturnsAllExactNameMatches()
        {
//            var category1 = new Category() {Name = "testName1"};
//            var category2 = new Category() {Name = "testName2"};
//            var category3 = new Category() {Name = "testName3"};
//            _categoryRepositoryInMemory.Add(category1);
//            _categoryRepositoryInMemory.Add(category2);
//            _categoryRepositoryInMemory.Add(category3);
//
//            var categoryMatched = _categoryQueryInMemory.ByExactName("testName2").First();
//
//            Assert.AreEqual(category2.Name, categoryMatched.Name);
        }

        [Test]
        public void CategoryQuery_ByPartialName_ReturnsAllPartialNameMatches()
        {
//            var category1 = new Category() {Name = "testName1"};
//            var category2 = new Category() {Name = "testName2"};
//            var category3 = new Category() {Name = "testName3"};
//            var category4 = new Category() {Name = "differentName"};
//            _categoryRepositoryInMemory.Add(category1);
//            _categoryRepositoryInMemory.Add(category2);
//            _categoryRepositoryInMemory.Add(category3);
//            _categoryRepositoryInMemory.Add(category4);
//
//            var categoryMatched = _categoryQueryInMemory.ByPartialName("testName");
//
//            Assert.AreEqual(categoryMatched.Count, 3);
        }

        #endregion
    }
}