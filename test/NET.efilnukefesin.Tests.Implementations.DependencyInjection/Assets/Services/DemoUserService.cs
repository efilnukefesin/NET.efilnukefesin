using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoUserService : BaseObject, IUserService
    {
        #region Properties

        private IRoleService roleService;
        private IDataService dataService;

        #endregion Properties

        #region Construction

        public DemoUserService(IRoleService roleService, IDataService dataService)
        {
            this.roleService = roleService;
            this.dataService = dataService;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.roleService = null;
            this.dataService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
