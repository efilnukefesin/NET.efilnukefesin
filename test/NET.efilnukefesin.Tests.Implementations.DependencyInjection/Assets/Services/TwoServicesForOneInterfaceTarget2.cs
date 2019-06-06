using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets
{
    internal class TwoServicesForOneInterfaceTarget2 : BaseObject
    {
        #region Properties

        public ITwoServicesForOneInterfaceService Service { get; private set; }

        #endregion Properties

        #region Construction

        public TwoServicesForOneInterfaceTarget2(ITwoServicesForOneInterfaceService service) : base()
        {
            this.Service = service;
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
