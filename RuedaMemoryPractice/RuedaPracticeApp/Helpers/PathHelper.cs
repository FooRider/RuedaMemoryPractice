using System;
using System.Collections.Generic;
using System.Text;

namespace FooRider.RuedaPracticeApp.Helpers
{
  public static class PathHelper
  {
    public static string MakeRelativePath(string rootAbsolute, string targetAbsolute)
    {
      if (!(rootAbsolute.EndsWith("/") || rootAbsolute.EndsWith("\\")))
        rootAbsolute += "\\";

      var rootUri = new Uri(rootAbsolute);
      var targetUri = new Uri(targetAbsolute);
      var targetRelativeUri = rootUri.MakeRelativeUri(targetUri);

      var targetRelative = targetRelativeUri.ToString().Replace('/', '\\');

      return Uri.UnescapeDataString(targetRelative);
    }
  }
}
