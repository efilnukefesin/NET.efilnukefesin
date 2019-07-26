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
        #region AskSpecialEndpoint
        [TestMethod]
        public async Task AskSpecialEndpoint()
        {
            DiSetup.RestDataServiceTests();
            DiSetup.InitializeRestEndpoints();

            this.startLocalServer();

            IDataService dataService = DiHelper.GetService<IDataService>(this.localServerUri, this.getHttpClientHandler());
            // TypedBaseClient<NET.efilnukefesin.Implementations.Base.ValueObject`1[System.Boolean]>.GetAsync(api/specialvalues//1/Hello World): entered

            var result = await dataService.GetAsync<ValueObject<bool>>("SpecialValueStore", "1", "Hello World");
            //TODO: ask an end point with a pecial function which does not appear under the Store concept

            Assert.IsNotNull(result);
            //Assert.AreEqual(3, result.Count());
        }
        #endregion AskSpecialEndpoint
    }
}
