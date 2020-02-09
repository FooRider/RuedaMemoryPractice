using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.RuedaPracticeApp.Contracts.Persistency
{
  public class PracticeSubject
  {
    public string PathBase { get; set; }
    public List<PracticeItem> Items { get; set; } = new List<PracticeItem>();
  }
}
