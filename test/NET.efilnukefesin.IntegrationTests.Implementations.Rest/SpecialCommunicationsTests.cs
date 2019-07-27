using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest
{
    [TestClass]
    public class SpecialCommunicationsTests : BaseHttpTest
    {
        #region RegularGets
        [TestMethod]
        public async Task RegularGets()
        {
            DiSetup.RestDataServiceTests();
            DiSetup.InitializeRestEndpoints();

            this.startLocalServer();

            IDataService dataService = DiHelper.GetService<IDataService>(this.localServerUri, this.getHttpClientHandler());

            var result = await dataService.GetAllAsync<ValueObject<string>>("SpecialValueStore");
            var resultItem = await dataService.GetAsync<ValueObject<string>>("SpecialValueStore", result.ToList()[1].Id);  //TODO: check, why ids do not match

            Assert.IsNotNull(result);
            Assert.IsNotNull(resultItem);
            Assert.AreEqual(3, result.Count());
        }
        #endregion RegularGets

        #region AskSpecialEndpoint
        [TestMethod]
        public async Task AskSpecialEndpoint()
        {
            DiSetup.RestDataServiceTests();
            DiSetup.InitializeRestEndpoints();

            this.startLocalServer();

            IDataService dataService = DiHelper.GetService<IDataService>(this.localServerUri, this.getHttpClientHandler());

            var result = await dataService.GetAsync<ValueObject<bool>>("SpecialValueStore", "1", "Hello World");

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Value);
        }
        #endregion AskSpecialEndpoint
    }
}
