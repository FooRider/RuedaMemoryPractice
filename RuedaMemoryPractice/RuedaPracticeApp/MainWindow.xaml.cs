using FooRider.RuedaPracticeApp.Contracts.Internal;
using FooRider.RuedaPracticeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FooRider.RuedaPracticeApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, IPlayerControls
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      ((MainVM)DataContext).Initialize(this);
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      ((MainVM)DataContext).OnClosing(sender, e);
    }

    private void Window_Closed(object sender, EventArgs e)
    {
      using ((MainVM)DataContext) { }
    }

    public void PlayMediaFile(string path)
    {
      MediaPlayer.Stop();
      MediaPlayer.Source = new Uri(path);
      MediaPlayer.Play();
    }

    public void Stop()
    {
      MediaPlayer.Stop();
    }
  }
}
