using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace FooRider.RuedaPracticeApp.Converters
{
  public class BooleanNotConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      => !((value as bool?) ?? false);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      => !((value as bool?) ?? false);
  }
}
