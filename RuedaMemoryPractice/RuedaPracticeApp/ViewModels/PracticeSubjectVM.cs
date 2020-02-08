using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.RuedaPracticeApp.ViewModels
{
  public class PracticeSubjectVM : BindableBase
  {
    private bool isDirty = false;
    public bool IsDirty
    {
      get => isDirty;
      set
      {
        if (isDirty != value)
        {
          isDirty = value;
          RaisePropertyChanged();
        }
      }
    }
  }
}
