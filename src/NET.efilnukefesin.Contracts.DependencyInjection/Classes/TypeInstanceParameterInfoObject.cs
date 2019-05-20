using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.DependencyInjection.Classes
{
    public class TypeInstanceParameterInfoObject : ParameterInfoObject
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

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Type = null;
            this.Instance = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
