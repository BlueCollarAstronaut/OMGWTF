using System;
using System.IO;
using System.Reflection;
using Randumberator.Core;
using Randumberator.Core.Abstractions;

namespace Randumberator.Plugins.NeverTheSameTwice
{
    [PluginFactory(typeof(Factory))]
    public class Plugin
        : PluginBase
    {
        private string LogPath;

        protected override bool AmIValid()
        {
            return true;
        }
        public override int Randomize(int pool)
        {
            if (pool == 0)
                return 0;

            int previousValue = ReadLog();
            var rnd = new Random();
            int newValue = previousValue;
            while (newValue == previousValue)
            {
                newValue = rnd.Next(0, pool + 1);
            }
            WriteLog(newValue);

            return newValue;
        }

        private int ReadLog()
        {
            try
            {
                ValidateLog();
                using (StreamReader reader = new StreamReader(LogPath))
                {
                    var rawValue = reader.ReadToEnd();
                    return int.Parse(rawValue);
                }
            }
            catch (Exception)
            {
                //  If there's a problem, return a safe value
                return -1;
            }
        }

        private void WriteLog(int value)
        {
            using (StreamWriter writer = new StreamWriter(LogPath))
            {
                writer.Write(value.ToString());
            }
        }

        private void ValidateLog()
        {
            // Determine log file based on the path and name for this plugin.
            var thisAss = Assembly.GetAssembly(typeof (Plugin));
            var assPath = Path.GetDirectoryName(thisAss.Location);
            string fileName = string.Format("{0}.rnd", thisAss.GetName() , ".rnd");
            LogPath = Path.Combine(assPath, fileName);

            if (!File.Exists(LogPath))
            {
                using (StreamWriter writer = new StreamWriter(LogPath))
                {
                    writer.Write("-1");
                }
            }
        }
    }
}
