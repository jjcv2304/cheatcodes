using Domain.Categories;

namespace Application.Categories.Tests
{
    public static class TestableCategoryBuilder
    {
        public static TestableCategory BuildOne(long id = 1, string name="TestCategory1", string description="first category test object")
        {
           return new TestableCategory(){ TestableId = id, Name = name, Description = description};
        }
    }
    
    public class TestableCategory : Category { 
        public long TestableId { 
            get => Id;
            set => Id = value;
        }
    }
}