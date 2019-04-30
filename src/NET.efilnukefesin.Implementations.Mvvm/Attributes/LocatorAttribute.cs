using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm.Attributes
{
    public class LocatorAttribute : Attribute
    {
        #region Properties
        public string Name { get; set; }
        #endregion Properties

        #region Construction

        public LocatorAttribute(string name)
        {
            this.Name = name;
        }
        #endregion Construction
    }
}
