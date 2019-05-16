using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.DependencyInjection.Classes
{
    public class TypeInstanceParameterInfoObject
    {
        #region Properties
        public Type Type { get; set; }
        public object Instance { get; set; }
        #endregion Properties

        #region Construction
        public TypeInstanceParameterInfoObject(Type type, object instance)
        {
            this.Type = type;
            this.Instance = instance;
        }
        #endregion Construction
    }
}
