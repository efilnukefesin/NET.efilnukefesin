using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TwoServicesForOneInterfaceService4 : BaseObject, ITwoServicesForOneInterfaceService
    {
        #region Properties

        private string someString;
        private ITwoServicesForOneInterfaceOtherService service;

        #endregion Properties

        #region Construction

        public TwoServicesForOneInterfaceService4(string SomeString, ITwoServicesForOneInterfaceOtherService service) : base()
        {
            this.someString = SomeString;
            this.service = service;
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
