using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TwoServicesForOneInterfaceService2 : BaseObject, ITwoServicesForOneInterfaceService
    {
        #region Properties

        private string someString;

        #endregion Properties

        #region Construction

        public TwoServicesForOneInterfaceService2(string SomeString) : base()
        {
            this.someString = SomeString;
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
