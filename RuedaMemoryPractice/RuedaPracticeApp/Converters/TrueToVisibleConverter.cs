using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace FooRider.RuedaPracticeApp.Converters
{
  public class TrueToVisibleConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      => ((value as bool?) ?? false) ? Visibility.Visible : Visibility.Collapsed;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      => ((value as Visibility?) ?? Visibility.Collapsed) == Visibility.Visible;
  }
}
