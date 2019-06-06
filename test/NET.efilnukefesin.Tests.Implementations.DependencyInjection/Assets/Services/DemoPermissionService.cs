using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoPermissionService : BaseObject, IPermissionService
    {
        #region Properties

        private IDataService dataService;

        #endregion Properties

        #region Construction

        public DemoPermissionService(IDataService dataService)
        {
            this.dataService = dataService;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.dataService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
