using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Plugins
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class PluginFactoryAttribute
        : Attribute
    {
        public Type FactoryType { get; private set; }


        public PluginFactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }


    }
}
