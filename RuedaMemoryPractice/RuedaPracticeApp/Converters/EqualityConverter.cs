using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace FooRider.RuedaPracticeApp.Converters
{
  public class EqualityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      => value.Equals(parameter);

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      => throw new NotImplementedException();
  }
}
