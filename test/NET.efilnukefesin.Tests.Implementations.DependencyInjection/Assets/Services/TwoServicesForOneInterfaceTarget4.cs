using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TwoServicesForOneInterfaceTarget4 : BaseObject
    {
        #region Properties

        public ITwoServicesForOneInterfaceService Service { get; private set; }
        public ITwoServicesForOneInterfaceOtherService Service2 { get; private set; }
        #endregion Properties

        #region Construction

        public TwoServicesForOneInterfaceTarget4(ITwoServicesForOneInterfaceService service, ITwoServicesForOneInterfaceOtherService service2) : base()
        {
            this.Service = service;
            this.Service2 = service2;
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
