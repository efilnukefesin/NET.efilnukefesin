using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoAuthenticationService : BaseObject
    {
        #region Properties

        private IUserService userService;
        private IDataService dataService;

        #endregion Properties

        #region Construction
        public DemoAuthenticationService(IUserService userService, IDataService dataService)
        {
            this.userService = userService;
            this.dataService = dataService;
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
