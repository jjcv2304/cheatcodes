using Application.Categories.Queries;
using Application.Categories.ViewModels;
using Application.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Categories.Tests
{
    [TestFixture]
    public class CategoryQueryTest
    {
        private CategoryQuery _categoryQuery;
        private Mock<ICategoryRepository> _categoryRepositoryMock;

        [SetUp]
        public void TestSetup()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryQuery = new CategoryQuery(_categoryRepositoryMock.Object);
        }
        
        [Test]
        public void ByName_ReturnsAllPartialMatches()
        {
            long testId = 1;
            var category = TestableCategoryBuilder.BuildOne();
            _categoryRepositoryMock.Setup(x => x.GetById(testId)).Returns(category);

            var categoryVM = _categoryQuery.ById(1);

            Assert.IsInstanceOf<CategoryVM>(categoryVM);
        }
    }
}