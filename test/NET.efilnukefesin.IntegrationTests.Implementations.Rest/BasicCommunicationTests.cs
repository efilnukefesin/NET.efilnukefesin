using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.BaseClasses.Test.Http;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest
{
    // https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api
    // https://docs.microsoft.com/de-de/aspnet/core/test/integration-tests?view=aspnetcore-2.2
    // https://github.com/willj/aspnet-core-mstest-integration-sample/blob/master/MSUnitTestProject1/UnitTest1.cs
    // https://www.codeproject.com/Articles/1197462/Using-MS-Test-with-NET-Core-API

    [TestClass]
    public class BasicCommunicationTests : BaseHttpTest<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup>
    {
        #region Methods

        #region SimpleCallWithStandardClient
        [TestMethod]
        public async Task SimpleCallWithStandardClient()
        {
            this.startLocalServer();

            HttpClient client = null;
            bool isError = false;
            HttpResponseMessage result = null;

            try
            {
                client = this.webApplicationFactory.CreateClient();
                result = await client.GetAsync("/api/values");
            }
            catch (Exception ex)
            {
                isError = true;
            }

            Assert.IsFalse(isError);
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccessStatusCode);
        }
        #endregion SimpleCallWithStandardClient

        #region SimpleCallWithDataService
        //[TestMethod]
        public async Task SimpleCallWithDataService()
        {
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
