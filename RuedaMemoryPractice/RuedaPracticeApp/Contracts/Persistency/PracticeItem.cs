using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.RuedaPracticeApp.Contracts.Persistency
{
  public class PracticeItem
  {
    public string Name { get; set; }
    public string RelativeMediaPath { get; set; }

    public int SuccessCount { get; set; } = 0;
    public int FailureCount { get; set; } = 0;
  }
}
