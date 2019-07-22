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
using System.Linq;
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
            #region messageHandlerMockFaker: creates a fake response for a faked http handler
            /// <summary>
            /// creates a fake response for a faked http handler
            /// </summary>
            /// <param name="response">the response to deliver</param>
            /// <returns>the mock object</returns>
            private Mock<HttpMessageHandler> messageHandlerMockFaker(HttpResponseMessage response)
            {
                Mock<HttpMessageHandler> result;

                result = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                result
                   .Protected()
                   // Setup the PROTECTED method to mock
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   // prepare the expected response of the mocked http call
                   .ReturnsAsync(response)
                   .Verifiable();

                return result;
            }
            #endregion messageHandlerMockFaker

            #region GetAsync
            [TestMethod]
            public void GetAsync()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(true)))),
                });

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken", handlerMock.Object);

                bool result = dataService.GetAsync<ValueObject<bool>>("TestResourceLocation").GetAwaiter().GetResult().Value;

                Assert.AreEqual(true, result);
            }
            #endregion GetAsync

            #region GetAllAsync
            [TestMethod]
            public void GetAllAsync()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<IEnumerable<ValueObject<bool>>>(new List<ValueObject<bool>>(new List<ValueObject<bool>>() { new ValueObject<bool>(true), new ValueObject<bool>(false), new ValueObject<bool>(true) })))),
                });

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken", handlerMock.Object);

                List<ValueObject<bool>> result = dataService.GetAllAsync<ValueObject<bool>>("TestResourceLocation").GetAwaiter().GetResult().ToList();

                Assert.AreEqual(true, result[0].Value);
            }
            #endregion GetAllAsync

            #region CreateOrUpdateAsync
            [TestMethod]
            public void CreateOrUpdateAsync()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<ValueObject<bool>>(new ValueObject<bool>(true)))),
                });

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://localhost"), "someToken25", handlerMock.Object);

                bool result = dataService.CreateOrUpdateAsync<ValueObject<bool>>("TestResourceLocation", new ValueObject<bool>(true)).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsync

            #region CreateOrUpdateAsyncWithDelegate
            [TestMethod]
            public void CreateOrUpdateAsyncWithDelegate()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<IEnumerable<ValueObject<bool>>>(new List<ValueObject<bool>>(new List<ValueObject<bool>>() { new ValueObject<bool>(true), new ValueObject<bool>(false), new ValueObject<bool>(true) })))),
                });

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://localhost"), "someToken25", handlerMock.Object);

                bool result = dataService.CreateOrUpdateAsync<ValueObject<bool>>("TestResourceLocation", new ValueObject<bool>(true), x => x.Value.Equals(false)).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncWithDelegate

            #region DeleteAsyncWithDelegate
            [TestMethod]
            public void DeleteAsyncWithDelegate()
            {
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<IEnumerable<ValueObject<bool>>>(new List<ValueObject<bool>>(new List<ValueObject<bool>>() { new ValueObject<bool>(true), new ValueObject<bool>(false), new ValueObject<bool>(true) })))),
                });

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://localhost"), "someToken25", handlerMock.Object);

                bool result = dataService.DeleteAsync<ValueObject<bool>>("TestResourceLocation", x => x.Value.Equals(true)).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsyncWithDelegate
        }
        #endregion DataServiceMethods
    }
}
