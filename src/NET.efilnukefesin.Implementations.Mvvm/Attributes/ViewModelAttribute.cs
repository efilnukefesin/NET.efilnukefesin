using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm.Attributes
{
    public class ViewModelAttribute : Attribute
    {
        #region Properties

        public string ViewModelName { get; set; }

        #endregion Properties

        #region Construction
        public ViewModelAttribute(string viewModelName)
        {
            this.ViewModelName = viewModelName;
        }

        #endregion Construction
    }
}
