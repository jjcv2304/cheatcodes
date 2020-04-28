namespace CheatCodes.Search.RabbitMQ.Models
{
  public class UpdateCategoryEvent
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = "";
  }
}