using Common.Utils;

namespace Common.Mocks
{
  public class MockEntity : Entity
  {
    public int TestableId
    {
      get => Id;
      set => Id = value;
    }
  }
}