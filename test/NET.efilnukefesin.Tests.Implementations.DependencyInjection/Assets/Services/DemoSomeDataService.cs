using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoSomeDataService : BaseObject, IDataService
    {
        #region Properties

        public int SomeNumber { get; set; }

        #endregion Properties

        #region Construction

        public DemoSomeDataService(int someNumber)
        {
            SomeNumber = someNumber;
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
