using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    public class TwoServicesForOneInterfaceService1 : BaseObject, ITwoServicesForOneInterfaceService
    {
        #region Properties

        private int someNumber;

        #endregion Properties

        #region Construction

        public TwoServicesForOneInterfaceService1(int SomeNumber) : base()
        {
            this.someNumber = SomeNumber;
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
