namespace CheatCodes.Search.RabbitMQ.Models
{
  public class NewCategoryEvent
  {
    public int Id { get; set; }
    public string Name { get;  set;}
    public string Description { get; set;}
    public int ParentId { get;set; }
  }
}
