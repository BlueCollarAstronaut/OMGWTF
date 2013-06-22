using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Randumberator.Core.Abstractions;
using Randumberator.Core.FailSafe;

namespace Randumberator.Core
{
    public static class Utilities
    {
        public static IPluginRepositoryFactory[] GetPluginRepositoryFactories(string path)
        {
            try
            {
                List<IPluginRepositoryFactory> oReturn = new List<IPluginRepositoryFactory>();
                foreach (var file in Directory.EnumerateFiles(path, "*.dll"))
                {
                    var assembly = Assembly.LoadFile(file);
                    var types = assembly.GetTypes().Where(x => x.GetInterface("IPluginRepository", false) != null).ToArray();
                    foreach (var type in types)
                    {
                        PluginRepositoryFactoryAttribute[] attributes = type.GetCustomAttributes(typeof(PluginRepositoryFactoryAttribute), false).Cast<PluginRepositoryFactoryAttribute>().ToArray();
                        if (attributes.Length == 1)
                        {
                            var ctor = attributes[0].FactoryType.GetConstructor(new Type[] { });
                            var factory = ctor.Invoke(new object[] { }) as IPluginRepositoryFactory;
                            oReturn.Add(factory);
                        }
                    }
                }
                return oReturn.ToArray();
            }
            catch (Exception)
            {
                return new  IPluginRepositoryFactory[]{new FailsafePluginRepositoryFactory()};
            }
        }

        public static ITheUserExperience GetTheUserExperience(string path)
        {
            var qualifiedName = typeof (ITheUserExperience).AssemblyQualifiedName;
            
            foreach (var file in Directory.EnumerateFiles(path, "*.dll"))
            {
                var assembly = Assembly.LoadFile(file);
                var types = 
                    assembly.GetTypes().Where(x => x.GetInterface("ITheUserExperience", false) != null).ToArray();
                if (types.Length > 0)
                {
                    var ctor = types[0].GetConstructor(new Type[] {});
                    return ctor.Invoke(new object[] {}) as ITheUserExperience;
                }
            }
            return null;
        }
    }
}
