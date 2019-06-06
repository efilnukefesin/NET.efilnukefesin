using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoRoleService : BaseObject, IRoleService
    {
        #region Properties

        private IPermissionService permissionService;
        private IDataService dataService;

        #endregion Properties

        #region Construction

        public DemoRoleService(IPermissionService permissionService, IDataService dataService)
        {
            this.permissionService = permissionService;
            this.dataService = dataService;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.permissionService = null;
            this.dataService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
