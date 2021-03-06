﻿using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoAuthenticationService : BaseObject
    {
        #region Properties

        public IUserService UserService { get; private set; }
        private IRoleService roleService;
        private IPermissionService permissionService;

        #endregion Properties

        #region Construction
        public DemoAuthenticationService(IUserService userService, IRoleService roleService, IPermissionService permissionService)
        {
            this.UserService = userService;
            this.roleService = roleService;
            this.permissionService = permissionService;
        }
        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.UserService = null;
            this.roleService = null;
            this.permissionService = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
