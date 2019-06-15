using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.BootStrapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.RestDataService
{
    [TestClass]
    public class RestDataServiceTests : BaseSimpleTest
    {
        #region DataServiceProperties
        [TestClass]
        public class RestDataServiceProperties : RestDataServiceTests
        {

        }
        #endregion DataServiceProperties

        #region DataServiceConstruction
        [TestClass]
        public class RestDataServiceConstruction : RestDataServiceTests
        {
            #region IsNotNull
            [TestMethod]
            public void IsNotNull()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken");

                Assert.IsNotNull(dataService);
                Assert.IsInstanceOfType(dataService, typeof(NET.efilnukefesin.Implementations.Services.DataService.RestDataService.RestDataService));
            }
            #endregion IsNotNull
        }
        #endregion DataServiceConstruction

        #region DataServiceMethods
        [TestClass]
        public class RestDataServiceMethods : RestDataServiceTests
        {
            #region GetAsync
            [TestMethod]
            public void GetAsync()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                // https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/
                // ARRANGE
                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                handlerMock
                   .Protected()
                   // Setup the PROTECTED method to mock
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   // prepare the expected response of the mocked http call
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(true)))),
                   })
                   .Verifiable();

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken", handlerMock.Object);

                bool result = dataService.GetAsync<ValueObject<bool>>("SomeAction").GetAwaiter().GetResult().Value;

                Assert.AreEqual(true, result);
            }
            #endregion GetAsync

            #region CreateOrUpdateAsync
            [TestMethod]
            public void CreateOrUpdateAsync()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                handlerMock
                   .Protected()
                   // Setup the PROTECTED method to mock
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   // prepare the expected response of the mocked http call
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(true)))),
                   })
                   .Verifiable();

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://localhost"), "someToken25", handlerMock.Object);

                bool result = dataService.CreateOrUpdateAsync<ValueObject<bool>>("SomeOtherAction", new ValueObject<bool>(true)).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsync
        }
        #endregion DataServiceMethods
    }
}
