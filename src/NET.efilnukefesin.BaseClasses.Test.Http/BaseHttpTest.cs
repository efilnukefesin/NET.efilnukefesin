using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test.Http.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET.efilnukefesin.BaseClasses.Test.Http
{
    [TestClass]
    public class BaseHttpTest<StartupType> : BaseSimpleTest where StartupType : class
    {
        #region Properties

        protected readonly CustomWebApplicationFactory<StartupType> webApplicationFactory;
        protected Uri localServerUri = new Uri("http://localhost/");

        #endregion Properties

        #region Construction
        public BaseHttpTest(string solutionRelativePath = null, string applicationBasePath = null, string solutionName = null, string Environment = "Development")
        {
            this.webApplicationFactory = new CustomWebApplicationFactory<StartupType>(solutionRelativePath, applicationBasePath, solutionName, Environment);
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
            this.webApplicationFactory.CreateClient();  //needed for getting up the server
        }
        #endregion startLocalServer

        #endregion Methods
    }
}
