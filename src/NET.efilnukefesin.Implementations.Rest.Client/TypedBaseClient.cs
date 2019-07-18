using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Client
{
    public abstract class TypedBaseClient<T> : BaseClient where T : IBaseObject
    {
        #region Properties

        #endregion Properties

        #region Construction

        public TypedBaseClient(ILogger Logger)
            : base(Logger)
        {

        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
