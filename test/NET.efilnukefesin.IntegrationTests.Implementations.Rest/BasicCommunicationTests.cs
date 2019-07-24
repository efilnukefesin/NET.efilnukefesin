using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;
using NET.efilnukefesin.IntegrationTests.Implementations.Rest.Classes;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest
{
    [TestClass]
    public class BasicCommunicationTests : BaseSimpleTest
    {
        #region Properties

        private readonly CustomWebApplicationFactory<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup> webApplicationFactory;
        private Uri localServerUri = new Uri("http://localhost/");

        #endregion Properties

        #region Construction
        public BasicCommunicationTests()
        {
            this.webApplicationFactory = new CustomWebApplicationFactory<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup>();
        }
        #endregion Construction

        #region Methods

        #region generateTestItems
        private List<ValueObject<string>> generateTestItems()
        {
            List<ValueObject<string>> result = new List<ValueObject<string>>();
            ValueObject<string> item1 = new ValueObject<string>("item1");
            ValueObject<string> item2 = new ValueObject<string>("item2");
            ValueObject<string> item3 = new ValueObject<string>("item3");
            result.Add(item1);
            result.Add(item2);
            result.Add(item3);
            return result;
        }
        #endregion generateTestItems

        #region getHttpClientHandler: creates the handler for integration testing
        /// <summary>
        /// creates the handler for integration testing
        /// </summary>
        /// <returns>the handler to override the httpClient default handler with</returns>
        private Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler getHttpClientHandler()
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
        private void startLocalServer()
        {
            this.webApplicationFactory.CreateClient();  //needed for getting up the server
        }
        #endregion startLocalServer

        #region SimpleCallWithStandardClient
        [TestMethod]
        public async Task SimpleCallWithStandardClient()
        {
            // https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
            // https://docs.microsoft.com/de-de/aspnet/core/test/integration-tests?view=aspnetcore-2.2
            // https://github.com/willj/aspnet-core-mstest-integration-sample/blob/master/MSUnitTestProject1/UnitTest1.cs
            // https://www.codeproject.com/Articles/1197462/Using-MS-Test-with-NET-Core-API
            this.startLocalServer();

            var client = this.webApplicationFactory.CreateClient();

            var result = await client.GetAsync("/api/values");

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }
        #endregion SimpleCallWithStandardClient

        #region SimpleCallWithDataService
        [TestMethod]
        public async Task SimpleCallWithDataService()
        {
            // https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
            // https://docs.microsoft.com/de-de/aspnet/core/test/integration-tests?view=aspnetcore-2.2
            // https://github.com/willj/aspnet-core-mstest-integration-sample/blob/master/MSUnitTestProject1/UnitTest1.cs
            // https://www.codeproject.com/Articles/1197462/Using-MS-Test-with-NET-Core-API
            DiSetup.RestDataServiceTests();
            DiSetup.InitializeRestEndpoints();

            this.startLocalServer();

            IDataService dataService = DiHelper.GetService<IDataService>(this.localServerUri, this.getHttpClientHandler());

            var result = await dataService.GetAllAsync<ValueObject<string>>("ValueStore");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }
        #endregion SimpleCallWithDataService

        #endregion Methods
    }
}
