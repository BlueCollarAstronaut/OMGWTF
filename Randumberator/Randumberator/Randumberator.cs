using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using Randumberator.Core;
using Randumberator.Core.Abstractions;
using Randumberator.Core.FailSafe;

namespace Randumberator
{
    public class Randumberator
    {
        public IPluginRepository PluginRepository { get; private set; }
        
        
        public Randumberator()
        {
            var args = System.Environment.GetCommandLineArgs();
            var rootPath = Path.GetDirectoryName(args[0]);
            Initialize(rootPath);
        }

        public void Initialize(string rootPath, ITheUserExperience userExperience = null)
        {
            var pluginPath = Path.Combine(rootPath, ConfigurationManager.AppSettings.Get("PlgDir"));
            var pluginRepositoryPath = Path.Combine(rootPath, ConfigurationManager.AppSettings.Get("PginRepoPath"));
            var pluginPickerPath = Path.Combine(rootPath, ConfigurationManager.AppSettings.Get("PluginPickerDirectory"));
            var theUserExperienceDirectory = Path.Combine(rootPath, ConfigurationManager.AppSettings.Get("UsrExpDir"));
            
            // Find The User Experience if available
            var theUserExperience = Utilities.GetTheUserExperience(theUserExperienceDirectory);
            
            // Find Repository factories available...
            var repositoryFactories = Utilities.GetPluginRepositoryFactories(pluginRepositoryPath);



            // ... if there are none, then use the fail safe, otherwise pick a random one to use
            if (repositoryFactories.Length == 0)
                repositoryFactories = new IPluginRepositoryFactory[] { new FailsafePluginRepositoryFactory() };

            if (repositoryFactories.Length == 1)
                PluginRepository = repositoryFactories[0].GetRepository();
            else
            {
                var selectedIndex = new Random().Next(pluginRepositoryPath.Length);
                PluginRepository = repositoryFactories[selectedIndex].GetRepository();
            }

            theUserExperience.Show();
        }



    }
}
