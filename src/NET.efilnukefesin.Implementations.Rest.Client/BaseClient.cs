using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Client.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace NET.efilnukefesin.Implementations.Rest.Client
{
    public abstract class BaseClient : BaseObject
    {
        #region Properties

        protected ILogger logger;
        protected HttpClient httpClient;
        protected RequestInfo requestInfo;

        #endregion Properties

        #region Construction

        public BaseClient(Uri ResourceUri, ILogger Logger, HttpMessageHandler OverrideMessageHandler = null)
        {
            this.logger = Logger;
            if (OverrideMessageHandler != null)
            {
                this.httpClient = new HttpClient(OverrideMessageHandler);
            }
            else
            {
                this.httpClient = new HttpClient();
            }
            this.httpClient.BaseAddress = ResourceUri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
