using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Randumberator.Core;
using System.Reflection;
using Randumberator.Core.Abstractions;
using Randumberator.Core.FailSafe;

namespace Randumberator.PluginRepositories.FileSystem
{
    public class Factory
        : IPluginRepositoryFactory
    {
        public IPluginRepository GetRepository()
        {
            try
            {
                // Assume this is a sub-folder of the plugins folder
                Assembly thisAss = Assembly.GetAssembly(this.GetType());
                string thisFolder = Path.GetDirectoryName(thisAss.Location);
                string parentFolder = Directory.GetParent(thisFolder).FullName;
                return new PluginRepository(parentFolder);
            }
            catch (Exception)
            {
                return new FailsafePluginRepository();
            }
       }
    }
}
