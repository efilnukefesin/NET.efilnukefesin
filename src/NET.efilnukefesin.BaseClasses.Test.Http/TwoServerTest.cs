using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NET.efilnukefesin.BaseClasses.Test.Http
{
    [TestClass]
    public class TwoServerTest<StartupType1, StartupType2> : BaseSimpleTest where StartupType1 : class where StartupType2 : class
    {
        #region Properties

        protected readonly CustomWebApplicationFactory<StartupType1> webApplicationFactory1;
        protected readonly CustomWebApplicationFactory<StartupType2> webApplicationFactory2;
        protected Uri localServerUri1 = new Uri("http://localhost1/");
        protected Uri localServerUri2 = new Uri("http://localhost2/");
        protected HttpClient localClient1;
        protected HttpClient localClient2;

        #endregion Properties

        #region Construction
        public TwoServerTest(HttpTestConfiguration httpTestConfiguration1 = null, HttpTestConfiguration httpTestConfiguration2 = null)
        {
            HttpTestConfiguration config1 = new HttpTestConfiguration();
            if (httpTestConfiguration1 != null)
            {
                config1 = httpTestConfiguration1;
            }
            HttpTestConfiguration config2 = new HttpTestConfiguration();
            if (httpTestConfiguration2 != null)
            {
                config2 = httpTestConfiguration2;
            }
            this.webApplicationFactory1 = new CustomWebApplicationFactory<StartupType1>(config1);
            this.webApplicationFactory2 = new CustomWebApplicationFactory<StartupType2>(config2);
        }
        #endregion Construction

        #region Methods

        #region getHttpClientHandler: creates the handler for integration testing
        /// <summary>
        /// creates the handler for integration testing
        /// </summary>
        /// <returns>the handler to override the httpClient default handler with</returns>
        protected Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler getHttpClientHandler()
        {
            Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler result = new Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler(7);
            Microsoft.AspNetCore.Mvc.Testing.Handlers.CookieContainerHandler innerHandler1 = new Microsoft.AspNetCore.Mvc.Testing.Handlers.CookieContainerHandler();
            Microsoft.AspNetCore.TestHost.ClientHandler innerHandler2 = (Microsoft.AspNetCore.TestHost.ClientHandler)this.webApplicationFactory1.Server.CreateHandler();  //TODO: check if one client handler is valid for all redirects
            innerHandler1.InnerHandler = innerHandler2;
            result.InnerHandler = innerHandler1;

            return result;
        }
        #endregion getHttpClientHandler

        #region startLocalServers
        protected void startLocalServers()
        {
            this.localClient1 = this.webApplicationFactory1.CreateClient();  //needed for getting up the server
            this.localClient2 = this.webApplicationFactory2.CreateClient();  //needed for getting up the server
        }
        #endregion startLocalServers

        #endregion Methods
    }
}
