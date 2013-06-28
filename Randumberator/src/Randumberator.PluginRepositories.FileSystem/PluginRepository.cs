using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Randumberator.Core;
using Randumberator.Core.Abstractions;
using Randumberator.Core.FailSafe;
using System.IO;
using System.Reflection;

namespace Randumberator.PluginRepositories.FileSystem
{
    [PluginRepositoryFactory(typeof(Factory))]
    public class PluginRepository
        : IPluginRepository
    {
        private PluginBase[] _plugins;
        private string rootPath;

        public PluginRepository(string path)
        {
            rootPath = path;
        }

        public PluginBase[] Plugins
        {
            get
            {
                if (_plugins == null)
                    _plugins = InitializePlugins();
                return _plugins;
            }
        }

        private PluginBase[] InitializePlugins()
        {
            List<PluginBase> oReturn = new List<PluginBase>();

            try
            {
                // Loop through items in the root folder looking for plugins
                foreach (var file in Directory.EnumerateFiles(rootPath, "*.dll"))
                {
                    var thatAss = Assembly.LoadFile(file);
                    foreach (var type in thatAss.GetTypes())
                    {
                        var attributes =
                            type.GetCustomAttributes(typeof (PluginFactoryAttribute), false)
                                .Cast<PluginFactoryAttribute>()
                                .ToArray();
                        if (attributes.Length == 1)
                        {
                            var ctor = attributes[0].FactoryType.GetConstructor(new Type[]{});
                            var factory = ctor.Invoke(new object[]{}) as IPluginFactory;
                            oReturn.Add(factory.GetPlugin());
                        }
                    }
                }

            }
            catch (Exception)
            {
                // Ignore error for now
            }

            // If the collection of plugins is null (due to error or otherwise), 
            // add the failsafe item
            if (!oReturn.Any())
            {
                var factory = new FailsafePluginFactory();
                oReturn.Add(factory.GetPlugin());
            }
            return oReturn.ToArray();
        }
    }
}
