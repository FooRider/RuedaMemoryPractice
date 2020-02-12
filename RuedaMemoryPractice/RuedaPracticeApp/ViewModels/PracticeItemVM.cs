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

    private PracticeSubjectVM parentSubject;
    public PracticeSubjectVM ParentSubject
    {
      get => parentSubject;
      set
      {
        if (parentSubject != value)
        {
          parentSubject = value;
          RaisePropertyChanged();
        }
      }
    }

    private int successCount = 0;
    public int SuccessCount
    {
      get => successCount;
      set
      {
        if (successCount != value)
        {
          successCount = value;
          RaisePropertyChanged();
          RaisePropertyChanged(nameof(SuccessPercentage));
        }
      }
    }

    private int failureCount = 0;
    public int FailureCount
    {
      get => failureCount;
      set
      {
        if (failureCount != value)
        {
          failureCount = value;
          RaisePropertyChanged();
          RaisePropertyChanged(nameof(SuccessPercentage));
        }
      }
    }

    public float SuccessPercentage
    {
      get
      {
        var total = SuccessCount + FailureCount;
        if (total == 0) return 0;
        return 100 * ((float)SuccessCount / (float)total);
      }
    }
  }
}
