using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Implementations.Rest.Client;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Tests.Implementations.Rest.Client.Assets;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using NET.efilnukefesin.Implementations.Base;
using Newtonsoft.Json;
using System.Linq;

namespace NET.efilnukefesin.Tests.Implementations.Rest.Client
{
    [TestClass]
    public class TypedBaseClientTests : BaseSimpleTest
    {
        #region TypedBaseClientProperties
        [TestClass]
        public class TypedBaseClientProperties : TypedBaseClientTests
        {

        }
        #endregion TypedBaseClientProperties

        #region TypedBaseClientConstruction
        [TestClass]
        public class TypedBaseClientConstruction : TypedBaseClientTests
        {
            #region Resolve
            [TestMethod]
            public void Resolve()
            {
                DiSetup.Tests();

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"));

                Assert.IsNotNull(client);
            }
            #endregion Resolve
        }
        #endregion TypedBaseClientConstruction

        #region TypedBaseClientMethods
        [TestClass]
        public class TypedBaseClientMethods : TypedBaseClientTests
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

            #region convertToSerializedResult
            private string convertToSerializedResult<T>(T Value)
            {
                return JsonConvert.SerializeObject(new SimpleResult<T>(Value));
            }
            #endregion convertToSerializedResult

            private HttpContent getContent<T>(T Value)
            {
                return new StringContent(this.convertToSerializedResult<T>(Value));
            }

            #region GetAll
            [TestMethod]
            public void GetAll()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = this.getContent<List<ValueObject<string>>>(new List<ValueObject<string>>() { new ValueObject<string>("Hello World"), new ValueObject<string>("Hello World"), new ValueObject<string>("Hello World") }),
                });

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                var result = client.GetAllAsync().GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count());
            }
            #endregion GetAll

            #region GetAsync
            [TestMethod]
            public void GetAsync()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = this.getContent<ValueObject<string>>(new ValueObject<string>("Hello World")),
                });

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                ValueObject<string> result = client.GetAsync(1).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual("Hello World", result.Value);
            }
            #endregion GetAsync

            #region ExistsAsync
            [TestMethod]
            public void ExistsAsync()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                });

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.ExistsAsync(1).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            #endregion ExistsAsync

            #region ExistsAsyncNotFound
            [TestMethod]
            public void ExistsAsyncNotFound()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                });

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.ExistsAsync(1).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(false, result);
            }
            #endregion ExistsAsyncNotFound

            #region DeleteAsync
            [TestMethod]
            public void DeleteAsync()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                });

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.DeleteAsync(1).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsync

            #region DeleteAsyncNotFound
            [TestMethod]
            public void DeleteAsyncNotFound()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound
                });

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.DeleteAsync(1).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(false, result);
            }
            #endregion DeleteAsyncNotFound

            #region CreateAsync
            [TestMethod]
            public void CreateAsync()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Accepted
                });

                var newItem = new ValueObject<string>("SomeThing");

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.CreateAsync(newItem).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            #endregion CreateAsync

            #region UpdateAsync
            [TestMethod]
            public void UpdateAsync()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Accepted
                });

                var newItem = new ValueObject<string>("SomeThing");

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.UpdateAsync(newItem, 1).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            #endregion UpdateAsync

            #region CreateOrUpdateAsync
            [TestMethod]
            public void CreateOrUpdateAsync()
            {
                DiSetup.Tests();

                var handlerMock = this.messageHandlerMockFaker(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Accepted
                });

                var newItem = new ValueObject<string>("SomeThing");

                TypedTestClient client = DiHelper.GetService<TypedTestClient>(new Uri("http://baseUri"), handlerMock.Object);
                bool result = client.CreateOrUpdateAsync(newItem).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsync
        }
        #endregion TypedBaseClientMethods
    }
}
