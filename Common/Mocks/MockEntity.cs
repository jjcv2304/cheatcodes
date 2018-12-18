using Common.Utils;

namespace Common.Mocks
{
    public class MockEntity: Entity
    {
        public long TestableId { 
            get => Id;
            set => Id = value;
        }
    }
}