using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NugetManualDownloader
{
    class MyLogger : NuGet.Common.ILogger
    {
        public void LogDebug(string data)
        {
            Console.WriteLine("DEBUG:" + data);
        }

        public void LogError(string data)
        {
            throw new Exception(data);
        }

        public void LogErrorSummary(string data)
        {
            Console.WriteLine("ERROR:" + data);
        }

        public void LogInformation(string data)
        {
            Console.WriteLine("INFO:" + data);
        }

        public void LogInformationSummary(string data)
        {
            Console.WriteLine("INFOSummary:" + data);
        }

        public void LogMinimal(string data)
        {
            Console.WriteLine("INFOMinimal:" + data);

        }

        public void LogVerbose(string data)
        {
            Console.WriteLine("INFOVerbose:" + data);

        }

        public void LogWarning(string data)
        {
            Console.WriteLine("WARNING:" + data);
        }
    }
}
