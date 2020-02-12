using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.RuedaPracticeApp.Contracts.Internal
{
  public interface IPlayerControls
  {
    void PlayMediaFile(string path);
    void Stop();
  }
}
