﻿using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Client
{
    public abstract class BaseClient : BaseObject
    {
        #region Properties

        protected ILogger logger;

        #endregion Properties

        #region Construction

        public BaseClient(ILogger Logger)
        {
            this.logger = Logger;
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
