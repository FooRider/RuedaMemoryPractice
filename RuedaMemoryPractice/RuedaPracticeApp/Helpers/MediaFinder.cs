using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FooRider.RuedaPracticeApp.Helpers
{
  public static class MediaFinder
  {
    private static string[] supportedMediaExtensions = new[]
    {
      ".mp4", ".mkv", ".avi"
    };

    public static IEnumerable<string> FindMediaFiles(string mediaFolder)
      => CrawlThroughDirectory(mediaFolder)
          .Select(path => PathHelper.MakeRelativePath(mediaFolder, path));

    private static IEnumerable<string> CrawlThroughDirectory(string mediaFolder)
    {
      var di = new DirectoryInfo(mediaFolder);
      if (!di.Exists) yield break;

      foreach (var fi in di.EnumerateFiles().Where(IsSupportedMediaFile))
        yield return fi.FullName;

      foreach (var di1 in di.EnumerateDirectories())
        foreach (var fp in CrawlThroughDirectory(di1.FullName))
          yield return fp;
    }

    private static bool IsSupportedMediaFile(FileInfo file)
      => supportedMediaExtensions.Contains(file.Extension.ToLowerInvariant());
  }
}
