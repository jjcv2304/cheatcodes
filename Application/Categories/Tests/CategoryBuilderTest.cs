using Domain.Categories;

namespace Application.Categories.Tests
{
    public static class CategoryBuilderTest
    {
        //todo use the builder for testing
        public static Category BuildOne(
            long id = 1,
            string name = "TestCategory1",
            string description = "first category test object")
        {
            return new Category() {Id = id, Name = name, Description = description};
        }
    }


}