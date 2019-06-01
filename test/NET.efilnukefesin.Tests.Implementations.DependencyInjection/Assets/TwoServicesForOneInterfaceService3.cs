using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TwoServicesForOneInterfaceService3 : BaseObject, ITwoServicesForOneInterfaceService
    {
        #region Properties

        private int someNumber;

        private ITwoServicesForOneInterfaceOtherService service;

        #endregion Properties

        #region Construction

        public TwoServicesForOneInterfaceService3(int SomeNumber, ITwoServicesForOneInterfaceOtherService service) : base()
        {
            this.someNumber = SomeNumber;
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
