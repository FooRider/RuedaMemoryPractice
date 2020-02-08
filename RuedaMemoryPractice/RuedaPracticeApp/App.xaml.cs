using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FooRider.RuedaPracticeApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    private void Application_Activated(object sender, EventArgs e)
    {
      Application.Current.DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
      MessageBox.Show(
        messageBoxText: $"An unexpected error occured\r\n{e.Exception.Message}",
        caption: "An application error has occurred", 
        button: MessageBoxButton.OK,
        icon: MessageBoxImage.Error);
    }
  }
}
