using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Client.Classes;
using System;
using System.Collections.Generic;
using System.Net;
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
        private Uri resourceUri;

        #endregion Properties

        #region Construction

        public BaseClient(Uri ResourceUri, ILogger Logger = null, HttpMessageHandler OverrideMessageHandler = null)
        {
            this.logger = Logger;
            this.resourceUri = ResourceUri;
            if (OverrideMessageHandler != null)
            {
                this.httpClient = new HttpClient(OverrideMessageHandler);
            }
            else
            {
                this.httpClient = new HttpClient();
            }
            this.httpClient.BaseAddress = this.resourceUri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient.DefaultRequestHeaders.ConnectionClose = false;  //keep open as long as possible: https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/
            //TODO: make configurable / Test:
            ServicePointManager.FindServicePoint(this.resourceUri).ConnectionLeaseTimeout = 60 * 100;  //in miliseconds
            this.requestInfo = new RequestInfo();
        }

        #endregion Construction

        #region Methods

        #region AddAuthenticationHeader
        public void AddAuthenticationHeader(string Value)
        {
            string type = "Bearer";
            if (!string.IsNullOrEmpty(Value))
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, Value);
            }
        }
        #endregion AddAuthenticationHeader

        #endregion Methods

        #region Events

        #endregion Events
    }
}
