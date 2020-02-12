using FooRider.RuedaPracticeApp.Contracts.Persistency;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

    private string pathBase;
    public string PathBase
    {
      get => pathBase;
      set
      {
        if (pathBase != value)
        {
          pathBase = value;
          RaisePropertyChanged();
        }
      }
    }

    private ObservableCollection<PracticeItemVM> items = new ObservableCollection<PracticeItemVM>();
    public ObservableCollection<PracticeItemVM> Items
    {
      get => items;
      set
      {
        if (items != value)
        {
          items = value;
          RaisePropertyChanged();
        }
      }
    }

    public PracticeSubject ToPracticeSubject()
    {
      var res = new PracticeSubject()
      {
        PathBase = PathBase,
        Items = Items.Select(pivm => new PracticeItem()
        {
          Name = pivm.Name,
          RelativeMediaPath = pivm.RelativeMediaPath,
          SuccessCount = pivm.SuccessCount,
          FailureCount = pivm.FailureCount,
        }).ToList(),
      };


      return res;
    }
  }
}
