using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Rest.Client.Assets
{
    internal class TypedTestClient : TypedBaseClient<ValueObject<string>>
    {
        #region Properties

        #endregion Properties

        #region Construction

        public TypedTestClient(Uri BaseUri, ILogger Logger, HttpMessageHandler OverrideMessageHandler = null) 
            : base(BaseUri, Logger, OverrideMessageHandler)
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
