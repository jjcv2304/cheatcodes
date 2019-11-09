using NUnit.Framework;

namespace Domain
{
    [TestFixture]
    public class CategoryTests
    {
        private readonly Category _category;
        private const int Id = 1;
        private const string Name = "Test";


        public CategoryTests()
        {
            _category = new Category();
        }

        [Test]
        public void TestSetAndGetName()
        {
            _category.Name = Name;

            Assert.That(_category.Name, 
                Is.EqualTo(Name));
        }
    }
}