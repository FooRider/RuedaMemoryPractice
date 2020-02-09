using FooRider.RuedaPracticeApp.Contracts.Persistency;
using FooRider.RuedaPracticeApp.Helpers;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace FooRider.RuedaPracticeApp.ViewModels
{
  public class MainVM : BindableBase, IDisposable
  {
    private OpenFileDialog openFileDialog = new OpenFileDialog()
    {
      AddExtension = true,
      DefaultExt = Constants.DefaultExtension,
      Title = "Open practice",
      Filter = $"Practice file (*.{Constants.DefaultExtension})|*.{Constants.DefaultExtension}|All files|*.*",
      CheckFileExists = true,
      CheckPathExists = true,
    };
    private SaveFileDialog saveFileDialog = new SaveFileDialog()
    {
      AddExtension = true,
      DefaultExt = Constants.DefaultExtension,
      Title = "Save practice",
      Filter = $"Practice file (*.{Constants.DefaultExtension})|*.{Constants.DefaultExtension }| All files|*.*",
      OverwritePrompt = true,
    };
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog()
    {
      Description = "Select folder containing practice media files",
      UseDescriptionForTitle = true,
    };

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

    private DelegateCommand openPracticeSubjectCmd;
    public DelegateCommand OpenPracticeSubjectCmd => openPracticeSubjectCmd ?? (openPracticeSubjectCmd = new DelegateCommand(OpenPracticeSubject, canExecuteMethod: () => true));

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

      if (!(saveFileDialog.ShowDialog() ?? false)) 
        return;

      var dialogRes = folderBrowserDialog.ShowDialog();

      if (!(dialogRes == System.Windows.Forms.DialogResult.OK || dialogRes == System.Windows.Forms.DialogResult.Yes))
        return;

      CreateNewPracticeSubject(saveFileDialog.FileName, folderBrowserDialog.SelectedPath);
    }

    private void SavePracticeSubject()
    {

    }

    private void SavePracticeSubjectAs()
    {

    }

    private void OpenPracticeSubject()
    {
      var e = new System.ComponentModel.CancelEventArgs();
      CheckPendingChanges(this, e);
      if (e.Cancel)
        return;
    }

    private void CreateNewPracticeSubject(string practiceFilePath, string mediaFolder)
    {
      var subj = new PracticeSubject()
      {
        PathBase = mediaFolder,
      };

      subj.Items.AddRange(
        MediaFinder.FindMediaFiles(mediaFolder)
          .Select(relPath => new PracticeItem()
          {
            Name = Path.GetFileNameWithoutExtension(relPath),
            RelativeMediaPath = relPath,
          }));

      Console.WriteLine(subj);
    }


    public void Dispose()
    {
      
    }
  }
}
