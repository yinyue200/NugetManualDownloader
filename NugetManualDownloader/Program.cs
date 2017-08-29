using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetManualDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Copyright © yinyue200.com 2017");
            if (args.Length == 0)
            {
                Console.WriteLine("Sample:nupkgfile -c cachefolder -m Defaultv3");
                return;
            }
            var mode = NuGet.Packaging.PackageSaveMode.Defaultv3;
            var cachefolderpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".nuget", "packages");
            var nupkgfilepath = args[0];
            if (nupkgfilepath.Length > 1)
            {
                string lastcommand = string.Empty;
                foreach(var one in args)
                {
                    if (one.StartsWith("-"))
                    {
                        lastcommand = one.Substring(1);
                    }
                    else if (!string.IsNullOrWhiteSpace(lastcommand))
                    {
                        switch (lastcommand)
                        {
                            case "c":
                                cachefolderpath = one;
                                break;
                            case "m":
                                mode = (NuGet.Packaging.PackageSaveMode)Enum.Parse(typeof(NuGet.Packaging.PackageSaveMode), one, true);
                                break;
                        }
                    }
                }
            }
            NugetCacheManager nugetCacheManager = new NugetCacheManager(cachefolderpath, mode);
            nugetCacheManager.Put(File.Open(nupkgfilepath, FileMode.Open));
        }

        
    }
}
