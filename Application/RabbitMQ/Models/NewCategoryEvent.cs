using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RabbitMQ.Models
{
  public class NewCategoryEvent
  {

    public int Id { get;  }
    public string Name { get;  }
    public string Description { get; }
    public int ParentId { get; }

    public NewCategoryEvent(int id, string name, string description, int parentId)
    {
      Id = id;
      Name = name;
      Description = description;
      ParentId = parentId;
    }
  }
}
