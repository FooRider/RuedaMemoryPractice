using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FooRider.RuedaPracticeApp.ViewModels
{
  public class MainVM : BindableBase, IDisposable
  {
    private PracticeSubjectVM currentPracticeSubject;
    public PracticeSubjectVM CurrentPracticeSubject
    {
      get => currentPracticeSubject;
      set
      {
        if (currentPracticeSubject != value)
        {
          currentPracticeSubject = value;
          RaisePropertyChanged();
        }
      }
    }

    private DelegateCommand newPracticeSubjectCmd;
    public DelegateCommand NewPracticeSubjectCmd => newPracticeSubjectCmd ?? (newPracticeSubjectCmd = new DelegateCommand(NewPracticeSubject, canExecuteMethod: () => true));

    private DelegateCommand savePracticeSubjectCmd;
    public DelegateCommand SavePracticeSubjectCmd => savePracticeSubjectCmd ?? (savePracticeSubjectCmd = new DelegateCommand(SavePracticeSubject, canExecuteMethod: () => CurrentPracticeSubject != null));

    private DelegateCommand savePracticeSubjectAsCmd;
    public DelegateCommand SavePracticeSubjectAsCmd => savePracticeSubjectAsCmd ?? (savePracticeSubjectAsCmd = new DelegateCommand(SavePracticeSubjectAs, canExecuteMethod: () => CurrentPracticeSubject != null));

    private DelegateCommand loadPracticeSubjectCmd;
    public DelegateCommand LoadPracticeSubjectCmd => loadPracticeSubjectCmd ?? (loadPracticeSubjectCmd = new DelegateCommand(LoadPracticeSubject, canExecuteMethod: () => true));

    public void Initialize()
    {

    }

    private void CheckPendingChanges(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (CurrentPracticeSubject?.IsDirty ?? false)
      {
        var res = MessageBox.Show(
          messageBoxText: "There are unsaved changes in current practice subject, do you want to save the changes?",
          caption: "Unsaved changes are present",
          button: MessageBoxButton.YesNoCancel,
          icon: MessageBoxImage.Warning);

        if (res == MessageBoxResult.Yes)
        {
          SavePracticeSubject();
        }
        else if (res == MessageBoxResult.No)
        {
          return;
        }
        else // (res == MessageBoxResult.Cancel)
        {
          e.Cancel = true;
          return;
        }
      }
    }

    public void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      CheckPendingChanges(sender, e);
    }

    private void NewPracticeSubject()
    {
      var e = new System.ComponentModel.CancelEventArgs();
      CheckPendingChanges(this, e);
      if (e.Cancel)
        return;

      
    }

    private void SavePracticeSubject()
    {

    }

    private void SavePracticeSubjectAs()
    {

    }

    private void LoadPracticeSubject()
    {
      var e = new System.ComponentModel.CancelEventArgs();
      CheckPendingChanges(this, e);
      if (e.Cancel)
        return;


    }

    public void Dispose()
    {
      
    }
  }
}
