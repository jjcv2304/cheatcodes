using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Security
{
  public class MyConfig
  {
    public string Environment { get; set; }
  }

  public enum Environments
  {
    Development,
    Live
  }
}
