using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET.efilnukefesin.IntegrationTests.Implementations.Rest
{
    [TestClass]
    public class BasicCommunicationTests : BaseSimpleTest
    {
        private WebApplicationFactory<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup> webApplicationFactory;

        #region Initialize
        [TestInitialize]
        public void Initialize()
        {
            this.webApplicationFactory = new WebApplicationFactory<NET.efilnukefesin.IntegrationTests.Implementations.Rest.Project.Startup>();
        }
        #endregion Initialize

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

        #region Create
        [TestMethod]
        public async Task Create()
        {
            // https://docs.microsoft.com/de-de/aspnet/core/test/integration-tests?view=aspnetcore-2.2
            // https://github.com/willj/aspnet-core-mstest-integration-sample/blob/master/MSUnitTestProject1/UnitTest1.cs
            DiSetup.RestDataServiceTests();
            DiSetup.InitializeRestEndpoints();

            // Arrange
//            var client = this.webApplicationFactory.CreateClient();

            // Act
            //var response = await this.webApplicationFactory.GetAsync("/");

            List<ValueObject<string>> items = this.generateTestItems();
            TypedBaseController<ValueObject<string>> controller = new TypedBaseController<ValueObject<string>>(items);

            IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://localhost"), "someToken");
        }
        #endregion Create
    }
}
