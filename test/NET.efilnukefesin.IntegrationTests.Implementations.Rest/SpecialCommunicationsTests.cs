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
        #region SimpleCallWithDataService
        [TestMethod]
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
    }
}
