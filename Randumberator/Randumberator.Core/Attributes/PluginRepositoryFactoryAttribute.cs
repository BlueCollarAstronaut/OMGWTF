using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Randumberator.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PluginRepositoryFactoryAttribute
        : Attribute
    {
        public Type FactoryType { get; private set; }


        public PluginRepositoryFactoryAttribute(Type factoryType)
        {
            FactoryType = factoryType;
        }
    }
}
