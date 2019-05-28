using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Services.DataService.EndpointRegister
{
    public class EndpointRegister : BaseObject, IEndpointRegister
    {
        #region Properties

        private Dictionary<string, string> endpoints;

        #endregion Properties

        #region Construction

        public EndpointRegister()
        {
            this.endpoints = new Dictionary<string, string>();
        }

        #endregion Construction

        #region Methods

        #region AddEndpoint
        public bool AddEndpoint(string Action, string Endpoint)
        {
            bool result = false;

            if (!this.endpoints.ContainsKey(Action))
            {
                this.endpoints.Add(Action, Endpoint);
                result = true;
            }

            return result;
        }
        #endregion AddEndpoint

        #region GetEndpoint
        public string GetEndpoint(string Action)
        {
            string result = null;

            if (this.endpoints.ContainsKey(Action))
            {
                result = this.endpoints[Action];
            }

            return result;
        }
        #endregion GetEndpoint

        #region dispose
        protected override void dispose()
        {
            this.endpoints.Clear();
            this.endpoints = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
