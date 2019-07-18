using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Rest.Client.Assets
{
    internal class TypedTestClient : TypedBaseClient<ValueObject<string>>
    {
        #region Properties

        #endregion Properties

        #region Construction

        public TypedTestClient(ILogger Logger) : base(Logger)
        {

        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods

        #region Events

        #endregion Events
    }
}
