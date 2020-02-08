using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.RuedaPracticeApp.ViewModels
{
  public class PracticeItemVM : BindableBase
  {
    private string name;
    public string Name
    {
      get => name;
      set
      {
        if (name != value)
        {
          name = value;
          RaisePropertyChanged();
        }
      }
    }

    private string relativeMediaPath;
    public string RelativeMediaPath
    {
      get => relativeMediaPath;
      set
      {
        if (relativeMediaPath != value)
        {
          relativeMediaPath = value;
          RaisePropertyChanged();
        }
      }
    }

  }
}
