using FooRider.RuedaPracticeApp.Contracts.GlobalSettings;
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
using System.Text.Json;
using System.Windows;

namespace FooRider.RuedaPracticeApp.ViewModels
{
  public class MainVM : BindableBase, IDisposable
  {
    private GlobalSettings globalSettings;

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
    private Random random = new Random();

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

    private CurrentItemVM currentItem = new CurrentItemVM();
    public CurrentItemVM CurrentItem
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

    private DelegateCommand newPracticeSubjectCmd;
    public DelegateCommand NewPracticeSubjectCmd => newPracticeSubjectCmd ?? (newPracticeSubjectCmd = new DelegateCommand(NewPracticeSubject, canExecuteMethod: () => true));

    private DelegateCommand savePracticeSubjectCmd;
    public DelegateCommand SavePracticeSubjectCmd => savePracticeSubjectCmd ?? (savePracticeSubjectCmd = new DelegateCommand(SavePracticeSubject, canExecuteMethod: () => CurrentPracticeSubject != null));

    private DelegateCommand savePracticeSubjectAsCmd;
    public DelegateCommand SavePracticeSubjectAsCmd => savePracticeSubjectAsCmd ?? (savePracticeSubjectAsCmd = new DelegateCommand(SavePracticeSubjectAs, canExecuteMethod: () => CurrentPracticeSubject != null));

    private DelegateCommand openPracticeSubjectCmd;
    public DelegateCommand OpenPracticeSubjectCmd => openPracticeSubjectCmd ?? (openPracticeSubjectCmd = new DelegateCommand(OpenPracticeSubject, canExecuteMethod: () => true));

    private DelegateCommand<PracticeItemVM> playItemCmd;
    public DelegateCommand<PracticeItemVM> PlayItemCmd => playItemCmd ?? (playItemCmd = new DelegateCommand<PracticeItemVM>(PlayItem));

    private DelegateCommand playRandomItemCmd;
    public DelegateCommand PlayRandomItemCmd => playRandomItemCmd ?? (playRandomItemCmd = new DelegateCommand(PlayRandomItem));

    public void Initialize()
    {
      LoadGlobalSettings();

      if (globalSettings == null)
        globalSettings = new GlobalSettings();

      if (!string.IsNullOrEmpty(globalSettings.LastPracticeSubjectPath)
        && File.Exists(globalSettings.LastPracticeSubjectPath))
        LoadPracticeSubject(globalSettings.LastPracticeSubjectPath);
    }

    private void LoadGlobalSettings()
    {
      try
      {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var settingsFilePath = Path.Combine(appDataFolder, Constants.GlobalSettingsFilename);
        if (File.Exists(settingsFilePath))
        {
          globalSettings = JsonSerializer.Deserialize<GlobalSettings>(File.ReadAllText(settingsFilePath));
        }
      }
      catch (Exception ex)
      {
        // TODO log errors
      }
    }

    private void SaveGlobalSettings()
    {
      try
      {
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var settingsFilePath = Path.Combine(appDataFolder, Constants.GlobalSettingsFilename);
        File.WriteAllText(settingsFilePath, JsonSerializer.Serialize<GlobalSettings>(globalSettings, new JsonSerializerOptions() { WriteIndented = true, }));
      }
      catch (Exception ex)
      {
        // TODO log errors
      }
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
      if (!e.Cancel)
        SaveGlobalSettings();
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

      if (!(openFileDialog.ShowDialog() ?? false))
        return;

      LoadPracticeSubject(openFileDialog.FileName);
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

      using (var fh = File.Open(practiceFilePath, FileMode.Create, FileAccess.Write, FileShare.Read))
      using (var writer = new Utf8JsonWriter(fh))
        JsonSerializer.Serialize<PracticeSubject>(
          writer,
          subj, 
          new JsonSerializerOptions() { WriteIndented = true, });

      LoadPracticeSubject(practiceFilePath);
    }

    private void LoadPracticeSubject(string pathToSubject)
    {
      globalSettings.LastPracticeSubjectPath = pathToSubject;

      var subj = JsonSerializer.Deserialize<PracticeSubject>(File.ReadAllText(pathToSubject)); // TODO deserialize using streams (or switch to Newtonsoft.Json?)

      var subjVm = new PracticeSubjectVM()
      {
        IsDirty = false,
        PathBase = subj.PathBase,
      };

      foreach (var i in subj.Items)
        subjVm.Items.Add(new PracticeItemVM()
        {
          Name = i.Name,
          RelativeMediaPath = i.RelativeMediaPath,
          ParentSubject = subjVm,
        });

      CurrentPracticeSubject = subjVm;
    }

    private void PlayItem(PracticeItemVM practiceItem)
    {
      CurrentItem.Set(practiceItem);
    }

    private void PlayRandomItem()
    {
      if (CurrentPracticeSubject?.Items == null) return;

      var itemCount = CurrentPracticeSubject.Items.Count;
      var randomItem = CurrentPracticeSubject.Items[random.Next(0, itemCount)];
      PlayItem(randomItem);
    }

    public void Dispose()
    {
      
    }
  }
}
