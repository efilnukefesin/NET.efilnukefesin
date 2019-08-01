using NET.efilnukefesin.BaseClasses.Test.Http.Classes;
using NET.efilnukefesin.BaseClasses.Test.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET.efilnukefesin.BaseClasses.Test.Http
{
    public class BaseHttpMultipleServersTest : BaseSimpleTest
    {
        #region Properties

        private Dictionary<Guid, ICustomWebApplicationFactory> servers;
        private Dictionary<Guid, Type> startups;
        private Dictionary<Guid, Uri> addresses;

        #endregion Properties

        #region Construction

        public BaseHttpMultipleServersTest()
            : base()
        {
            this.servers = new Dictionary<Guid, ICustomWebApplicationFactory>();
            this.startups = new Dictionary<Guid, Type>();
            this.addresses = new Dictionary<Guid, Uri>();
        }

        #endregion Construction

        #region Methods

        #region AddServer
        protected Guid AddServer<StartupType>(HttpTestConfiguration config, bool DoStartServerAfterwards = true) where StartupType : class
        {
            Guid result = Guid.NewGuid();

            this.startups.Add(result, typeof(StartupType));
            this.servers.Add(result, new CustomWebApplicationFactory<StartupType>(config));

            if (DoStartServerAfterwards)
            {
                this.startLocalServer(result);
            }

            return result;
        }
        #endregion AddServer

        #region getHttpClientHandler: creates the handler for integration testing
        /// <summary>
        /// creates the handler for integration testing
        /// </summary>
        /// <returns>the handler to override the httpClient default handler with</returns>
        protected Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler getHttpClientHandler(Guid ServerId)
        {
            Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler result = default;

            if (this.servers.ContainsKey(ServerId))
            {
                result = new Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler(7);
                Microsoft.AspNetCore.Mvc.Testing.Handlers.CookieContainerHandler innerHandler1 = new Microsoft.AspNetCore.Mvc.Testing.Handlers.CookieContainerHandler();
                Microsoft.AspNetCore.TestHost.ClientHandler innerHandler2 = (Microsoft.AspNetCore.TestHost.ClientHandler)this.servers[ServerId].GetServer().CreateHandler();
                innerHandler1.InnerHandler = innerHandler2;
                result.InnerHandler = innerHandler1;
            }
            return result;
        }
        #endregion getHttpClientHandler

        #region startLocalServer
        protected void startLocalServer(Guid ServerId)
        {
            if (this.servers.ContainsKey(ServerId))
            {
                var client = this.servers[ServerId].GetClient(); //needed for getting up the server
                this.addresses.Add(ServerId, client.BaseAddress);
            }
        }
        #endregion startLocalServer

        #region getStartupType
        protected Type getStartupType(Guid ServerId)
        {
            Type result = default;

            if (this.startups.ContainsKey(ServerId))
            {
                result = this.startups[ServerId];
            }

            return result;
        }
        #endregion getStartupType

        #endregion Methods
    }
}
