using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm.Attributes
{
    public class ViewAttribute : Attribute
    {
        #region Properties
        public string ViewUri { get; set; }
        #endregion Properties

        #region Construction
        public ViewAttribute(string ViewUri)
        {
            this.ViewUri = ViewUri;
        }
        #endregion Construction
    }
}
