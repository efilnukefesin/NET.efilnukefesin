﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NET.efilnukefesin.BaseClasses.Test.Http
{
    [TestClass]
    public class BaseHttpTest<StartupType> : BaseSimpleTest where StartupType : class
    {
        #region Properties

        protected readonly CustomWebApplicationFactory<StartupType> webApplicationFactory;
        protected Uri localServerUri = new Uri("http://localhost/");
        protected HttpClient localClient;

        #endregion Properties

        #region Construction
        public BaseHttpTest(HttpTestConfiguration httpTestConfiguration = null)
        {
            HttpTestConfiguration config = new HttpTestConfiguration();
            if (httpTestConfiguration != null)
            {
                config = httpTestConfiguration;
            }
            this.webApplicationFactory = new CustomWebApplicationFactory<StartupType>(config);
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
            Microsoft.AspNetCore.TestHost.ClientHandler innerHandler2 = (Microsoft.AspNetCore.TestHost.ClientHandler)this.webApplicationFactory.Server.CreateHandler();
            innerHandler1.InnerHandler = innerHandler2;
            result.InnerHandler = innerHandler1;

            return result;
        }
        #endregion getHttpClientHandler

        #region startLocalServer
        protected void startLocalServer()
        {
            this.localClient = this.webApplicationFactory.CreateClient();  //needed for getting up the server
            this.localServerUri = this.localClient.BaseAddress;
        }
        #endregion startLocalServer

        #endregion Methods
    }
}
