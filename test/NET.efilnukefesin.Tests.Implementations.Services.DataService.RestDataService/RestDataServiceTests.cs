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

            #region GetAllAsync
            [TestMethod]
            public void GetAllAsync()
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
                       Content = new StringContent(JsonConvert.SerializeObject(new SimpleResult<IEnumerable<ValueObject<bool>>>(new List<ValueObject<bool>>( new List<ValueObject<bool>>() { new ValueObject<bool>(true), new ValueObject<bool>(false), new ValueObject<bool>(true) })))),
                   })
                   .Verifiable();

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken", handlerMock.Object);

                List<ValueObject<bool>> result = dataService.GetAllAsync<ValueObject<bool>>("SomeAction").GetAwaiter().GetResult().ToList();

                Assert.AreEqual(true, result[0].Value);
            }
            #endregion GetAllAsync

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

            #region CreateOrUpdateAsyncWithDelegate
            [TestMethod]
            public void CreateOrUpdateAsyncWithDelegate()
            {
                throw new NotImplementedException();
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();
                List<ValueObject<string>> items = new List<ValueObject<string>>();
                items.Add(new ValueObject<string>("TestString1"));
                items.Add(new ValueObject<string>("TestString2"));
                items.Add(new ValueObject<string>("TestString3"));
                items.Add(new ValueObject<string>("TestString4"));

                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", items).GetAwaiter().GetResult();

                bool resultAdd = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest1Action", new ValueObject<string>("TestString"), x => x.Value.Equals("DunnoYet")).GetAwaiter().GetResult();
                bool resultUpdate = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest1Action", new ValueObject<string>("TestString"), x => x.Value.Equals("TestString4")).GetAwaiter().GetResult();

                int numberafterwards = dataService.GetAllAsync<ValueObject<string>>("CreateOrUpdateAsyncTest1Action").GetAwaiter().GetResult().Count();

                Assert.AreEqual(true, resultAdd);
                Assert.AreEqual(true, resultUpdate);
                Assert.AreEqual(5, numberafterwards);
            }
            #endregion CreateOrUpdateAsyncWithDelegate

            #region DeleteAsyncWithDelegate
            [TestMethod]
            public void DeleteAsyncWithDelegate()
            {
                throw new NotImplementedException();
                DiSetup.RestDataServiceTests();
                DiSetup.InitializeRestEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();
                List<ValueObject<string>> items = new List<ValueObject<string>>();
                items.Add(new ValueObject<string>("TestString1"));
                items.Add(new ValueObject<string>("TestString2"));
                items.Add(new ValueObject<string>("TestString3"));
                items.Add(new ValueObject<string>("TestString4"));

                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", items).GetAwaiter().GetResult();
                bool result = dataService.DeleteAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", x => x.Value.Equals("TestString3")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsyncWithDelegate
        }
        #endregion DataServiceMethods
    }
}
