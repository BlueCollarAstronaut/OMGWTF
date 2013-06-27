using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Randumberator.Core;

namespace WPFRandumberator
{
    public static class MethodsAndStuff
    {
        public static int ConvertCentisecondsToMilliseconds(int centiseconds)
        {
            // Conversion factor to convert centiseconds to seconds
            double centisecondConversionFactor = (100d / 1d);         // 100 cs / 1s

            // Conversion factor to convert milliseconds to seconds
            double millisecondsInSecond = (1000d / 1d);               // 1,000 ms / 1s

            // This may be a bit confusing at first glance.   The following takes place
            // 1) seconds is calculated by dividing the number of centiseconds by the conversion factor above
            // 2) seconds is converted to milliseconds by multiplying by the other conversion factor above.
            int seconds = (int)(centiseconds / centisecondConversionFactor);
            int milliseconds = (int)(seconds * millisecondsInSecond);

            return milliseconds;
        }

        public static string GetPath(string suggestedPath, string rootPath = "")
        {
            if (rootPath == "")
                rootPath = Utilities.GetMyPath(typeof(MethodsAndStuff));

            return Path.IsPathRooted(suggestedPath) ?
                suggestedPath :
                Path.Combine(rootPath, suggestedPath);
        }
    }
}
