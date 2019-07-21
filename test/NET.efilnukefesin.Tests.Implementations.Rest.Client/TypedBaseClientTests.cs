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
                TypedTestClient client = DiHelper.GetService<TypedTestClient>();

                throw new NotImplementedException();
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

            #region Delete
            [TestMethod]
            public void Delete()
            {
                DiSetup.Tests();
                TypedTestClient client = DiHelper.GetService<TypedTestClient>();

                throw new NotImplementedException();
            }
            #endregion Delete

            #region Exists
            [TestMethod]
            public void Exists()
            {
                DiSetup.Tests();
                TypedTestClient client = DiHelper.GetService<TypedTestClient>();

                throw new NotImplementedException();
            }
            #endregion Exists

            #region Create
            [TestMethod]
            public void Create()
            {
                DiSetup.Tests();
                TypedTestClient client = DiHelper.GetService<TypedTestClient>();

                throw new NotImplementedException();
            }
            #endregion Create

            #region Update
            [TestMethod]
            public void Update()
            {
                DiSetup.Tests();
                TypedTestClient client = DiHelper.GetService<TypedTestClient>();

                throw new NotImplementedException();
            }
            #endregion Update
        }
        #endregion TypedBaseClientMethods
    }
}
