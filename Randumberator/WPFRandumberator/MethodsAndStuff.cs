using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFRandumberator
{
    public static class MethodsAndStuff
    {
        public static int ConvertCentisecondsToMilliseconds(int centiseconds)
        {
            double centisecondConversionFactor = (100d / 1d);         // 100 cs / 1s
            double millisecondsInSecond = (1000d / 1d);               // 1,000 ms / 1s

            int seconds = (int)(centiseconds / centisecondConversionFactor);
            int milliseconds = (int)(seconds * millisecondsInSecond);

            return milliseconds;

        }
    }
}
