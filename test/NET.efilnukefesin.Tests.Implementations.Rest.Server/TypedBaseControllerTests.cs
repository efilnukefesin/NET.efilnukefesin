using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Helpers;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.Rest.Server.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Rest.Server
{
    [TestClass]
    public class TypedBaseControllerTests : BaseSimpleTest
    {
        #region TypedBaseControllerProperties
        [TestClass]
        public class TypedBaseControllerProperties : TypedBaseControllerTests
        {

        }
        #endregion TypedBaseControllerProperties

        #region TypedBaseControllerConstruction
        [TestClass]
        public class TypedBaseControllerConstruction : TypedBaseControllerTests
        {
            #region Create
            [TestMethod]
            public void Create()
            {
                TypedTestController controller = new TypedTestController();

                bool hasApiControllerAttribute = TypeHelper.HasAttribute<ApiControllerAttribute>(typeof(TypedTestController));
                bool hasRouteAttribute = TypeHelper.HasAttribute<RouteAttribute>(typeof(TypedTestController));

                Assert.IsNotNull(controller);
                Assert.IsTrue(hasApiControllerAttribute);
                Assert.IsTrue(hasRouteAttribute);
            }
            #endregion Create
        }
        #endregion TypedBaseControllerConstruction

        #region TypedBaseControllerMethods
        [TestClass]
        public class TypedBaseControllerMethods : TypedBaseControllerTests
        {
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

            #region GetAll
            [TestMethod]
            public void GetAll()
            {
                TypedTestController controller = new TypedTestController();

                throw new NotImplementedException();
            }
            #endregion GetAll

            #region Get
            [TestMethod]
            public void Get()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                //bool hasHttpGetAttribute = this.hasAttribute<HttpGetAttribute>(typeof(TypedTestController));
                var result = controller.Get(items[1].Id).Value;

                Assert.IsNotNull(result);
                //Assert.IsTrue(hasHttpGetAttribute);
                Assert.IsInstanceOfType(result, typeof(SimpleResult<ValueObject<string>>));
                Assert.AreEqual(false, result.IsError);
                Assert.IsNotNull(result.Payload);
                Assert.IsNull(result.Error);
                Assert.AreEqual("item2", result.Payload.Value);
            }
            #endregion Get

            #region GetNotFound
            [TestMethod]
            public void GetNotFound()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.Get("SomeUnknownId").Value;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(SimpleResult<ValueObject<string>>));
                Assert.AreEqual(true, result.IsError);
                Assert.IsNull(result.Payload);
                Assert.IsNotNull(result.Error);
                Assert.AreEqual(1, result.Error.ErrorId);
            }
            #endregion GetNotFound

            #region Delete
            [TestMethod]
            public void Delete()
            {
                TypedTestController controller = new TypedTestController();

                throw new NotImplementedException();
            }
            #endregion Delete

            #region Exists
            [TestMethod]
            public void Exists()
            {
                TypedTestController controller = new TypedTestController();

                throw new NotImplementedException();
            }
            #endregion Exists

            #region Create
            [TestMethod]
            public void Create()
            {
                TypedTestController controller = new TypedTestController();

                throw new NotImplementedException();
            }
            #endregion Create

            #region Update
            [TestMethod]
            public void Update()
            {
                TypedTestController controller = new TypedTestController();

                throw new NotImplementedException();
            }
            #endregion Update
        }
        #endregion TypedBaseControllerMethods
    }
}
