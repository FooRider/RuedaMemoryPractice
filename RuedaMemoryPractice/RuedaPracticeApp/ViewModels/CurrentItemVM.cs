using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FooRider.RuedaPracticeApp.ViewModels
{
  public class CurrentItemVM : BindableBase
  {
    private CurrentItemState state;
    public CurrentItemState State
    {
      get => state;
      set
      {
        if (state != value)
        {
          state = value;
          RaisePropertyChanged();
        }
      }
    }

    private string currentMediaFilePath;
    public string CurrentMediaFilePath
    { 
      get => currentMediaFilePath;
      set
      {
        if (currentMediaFilePath != value)
        {
          currentMediaFilePath = value;
          RaisePropertyChanged();
        }
      }
    }



    public void Set(PracticeItemVM practiceItem)
    {
      var fullPathToMedia = Path.Combine(practiceItem.ParentSubject.PathBase, practiceItem.RelativeMediaPath);
      CurrentMediaFilePath = fullPathToMedia;
    }
  }
}
