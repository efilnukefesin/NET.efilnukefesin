using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Mvvm.Classes
{
    internal class NavigationInfo : BaseObject
    {
        #region Properties

        public string ViewmodelName { get; set; }
        public string ViewName { get; set; }
        public bool WasSuccessful { get; set; }

        #endregion Properties

        #region Construction

        public NavigationInfo(string viewmodelName, string viewName, bool wasSuccessful)
        {
            this.ViewmodelName = viewmodelName ?? throw new ArgumentNullException(nameof(viewmodelName));
            this.ViewName = viewName ?? throw new ArgumentNullException(nameof(viewName));
            this.WasSuccessful = wasSuccessful;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods
    }
}
