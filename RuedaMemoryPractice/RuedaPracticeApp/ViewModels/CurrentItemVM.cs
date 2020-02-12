using FooRider.RuedaPracticeApp.Contracts.Internal;
using Prism.Commands;
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

    public IPlayerControls Player { get; internal set; }

    private PracticeItemVM currentItem;
    public PracticeItemVM CurrentItem
    {
      get => currentItem;
      set
      {
        if (currentItem != value)
        {
          currentItem = value;
          RaisePropertyChanged();
        }
      }
    }

    private DelegateCommand checkCmd;
    public DelegateCommand CheckCmd => checkCmd ?? (checkCmd = new DelegateCommand(Check));

    private DelegateCommand recallSuccessCmd;
    public DelegateCommand RecallSuccessCmd => recallSuccessCmd ?? (recallSuccessCmd = new DelegateCommand(RecallSuccess));

    private DelegateCommand recallFailureCmd;
    public DelegateCommand RecallFailureCmd => recallFailureCmd ?? (recallFailureCmd = new DelegateCommand(RecallFailure));

    public void Set(PracticeItemVM practiceItem)
    {
      Player.Stop();

      State = CurrentItemState.Initial;
      CurrentItem = practiceItem;
    }

    private void Check()
    {
      Play();
    }

    private void RecallSuccess()
    {
      if (CurrentItem == null) return;

      CurrentItem.SuccessCount++;
      CurrentItem.ParentSubject.IsDirty = true;
    }

    private void RecallFailure()
    {
      if (CurrentItem == null) return;

      CurrentItem.FailureCount++;
      CurrentItem.ParentSubject.IsDirty = true;
    }



    private void Play()
    {
      if (CurrentItem == null)
        return;

      State = CurrentItemState.Playing;
      var fullPathToMedia = Path.Combine(CurrentItem.ParentSubject.PathBase, CurrentItem.RelativeMediaPath);
      //CurrentMediaFilePath = fullPathToMedia;
      Player.PlayMediaFile(fullPathToMedia);
    }
  }
}
