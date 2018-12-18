using System.Linq;
using Application.Categories.Queries;
using Application.Categories.ViewModels;
using Application.Interfaces;
using Application.Interfaces.Tests;
using Domain.Categories;
using Moq;
using NUnit.Framework;

namespace Application.Categories.Tests
{
    [TestFixture]
    public class CategoryQueryTest
    {
        private CategoryQuery _categoryQueryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private InMemoryCategoryRepository _categoryRepositoryInMemory;
        private CategoryQuery _categoryQueryInMemory;

        [SetUp]
        public void TestSetup()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryQueryMock = new CategoryQuery(_categoryRepositoryMock.Object);

            _categoryRepositoryInMemory = new InMemoryCategoryRepository();
            _categoryQueryInMemory = new CategoryQuery(_categoryRepositoryInMemory);
        }

        [Test]
        public void CategoryQuery_ReturnsVM()
        {
            const long testId = 2;
            var category = CategoryBuilderTest.BuildOne(testId);
            _categoryRepositoryMock.Setup(x => x.GetById(testId)).Returns(category);

            var categoryVM = _categoryQueryMock.ById(testId);

            Assert.IsInstanceOf<CategoryVM>(categoryVM);
        }

        [Test]
        public void CategoryQuery_ByExactName_ReturnsAllExactNameMatches()
        {
            var category1 = new Category() {Name = "testName1"};
            var category2 = new Category() {Name = "testName2"};
            var category3 = new Category() {Name = "testName3"};
            _categoryRepositoryInMemory.Add(category1);
            _categoryRepositoryInMemory.Add(category2);
            _categoryRepositoryInMemory.Add(category3);

            var categoryMatched = _categoryQueryInMemory.ByExactName("testName2");
            
            Assert.AreEqual(category2.Name, categoryMatched.FirstOrDefault().Name);
        }
        
        [Test]
        public void CategoryQuery_ByPartialName_ReturnsAllExactNameMatches()
        {
            var category1 = new Category() {Name = "testName1"};
            var category2 = new Category() {Name = "testName2"};
            var category3 = new Category() {Name = "testName3"};
            var category4 = new Category() {Name = "differentName"};
            _categoryRepositoryInMemory.Add(category1);
            _categoryRepositoryInMemory.Add(category2);
            _categoryRepositoryInMemory.Add(category3);
            _categoryRepositoryInMemory.Add(category4);

            var categoryMatched = _categoryQueryInMemory.ByPartialName("testName");
            
            Assert.AreEqual(categoryMatched.Count, 3);
        }
    }
}