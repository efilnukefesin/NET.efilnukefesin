using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoSomeDataService2 : BaseObject, IDataService
    {
        #region Properties

        public int SomeNumber { get; set; }
        public ILogger Logger { get; set; }

        #endregion Properties

        #region Construction

        public DemoSomeDataService2(int someNumber, ILogger logger)
        {
            SomeNumber = someNumber;
            this.Logger = logger;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Logger = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
