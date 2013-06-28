using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Plugins
{
    // This provides and extensible means delegating the responsiblity of 
    // determining randomness
    public abstract class PluginBase
    {
        public PluginBase()
        { }
        
        public bool IsValid()
        {
            try
            {
                // Leave it to the plugin to determine if it's valid or not
                return AmIValid();
            }
            catch (Exception)
            {
                // when in doubt, assume it's not
                return false;
            }
        }

        protected abstract bool AmIValid();

        public abstract int Randomize(int pool);


    }
}
