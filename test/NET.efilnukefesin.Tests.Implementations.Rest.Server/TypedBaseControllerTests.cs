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

                Assert.IsNotNull(controller);
            }
            #endregion Create

            #region ControllerHasRightAttributes
            [TestMethod]
            public void ControllerHasRightAttributes()
            {
                bool hasApiControllerAttribute = TypeHelper.HasAttribute<ApiControllerAttribute>(typeof(TypedTestController));
                bool hasRouteAttribute = TypeHelper.HasAttribute<RouteAttribute>(typeof(TypedTestController));

                Assert.IsTrue(hasApiControllerAttribute);
                Assert.IsTrue(hasRouteAttribute);
            }
            #endregion ControllerHasRightAttributes
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

            #region GetAllHasRightAttributes
            [TestMethod]
            public void GetAllHasRightAttributes()
            {
                bool hasHttpGetAttribute = MethodHelper.HasAttribute<HttpGetAttribute>(typeof(TypedTestController), "GetAll");

                Assert.IsTrue(hasHttpGetAttribute);
            }
            #endregion GetAllHasRightAttributes

            #region GetAll
            [TestMethod]
            public void GetAll()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.GetAll().Value;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(SimpleResult<IEnumerable<ValueObject<string>>>));
                Assert.AreEqual(false, result.IsError);
                Assert.IsNotNull(result.Payload);
                Assert.IsNull(result.Error);
                Assert.AreEqual(3, result.Payload.Count());
            }
            #endregion GetAll

            #region GetAllEmpty
            [TestMethod]
            public void GetAllEmpty()
            {
                TypedTestController controller = new TypedTestController();

                var result = controller.GetAll().Value;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(SimpleResult<IEnumerable<ValueObject<string>>>));
                Assert.AreEqual(true, result.IsError);
                Assert.IsNull(result.Payload);
                Assert.IsNotNull(result.Error);
                Assert.AreEqual(2, result.Error.ErrorId);
            }
            #endregion GetAllEmpty

            #region GetHasRightAttributes
            [TestMethod]
            public void GetHasRightAttributes()
            {
                bool hasHttpGetAttribute = MethodHelper.HasAttribute<HttpGetAttribute>(typeof(TypedTestController), "Get");

                Assert.IsTrue(hasHttpGetAttribute);
            }
            #endregion GetHasRightAttributes

            #region Get
            [TestMethod]
            public void Get()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.Get(items[1].Id).Value;

                Assert.IsNotNull(result);
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

                var result = controller.Get(Guid.NewGuid()).Value;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(SimpleResult<ValueObject<string>>));
                Assert.AreEqual(true, result.IsError);
                Assert.IsNull(result.Payload);
                Assert.IsNotNull(result.Error);
                Assert.AreEqual(1, result.Error.ErrorId);
            }
            #endregion GetNotFound

            #region HeadHasRightAttributes
            [TestMethod]
            public void HeadHasRightAttributes()
            {
                bool hasHttpHeadAttribute = MethodHelper.HasAttribute<HttpHeadAttribute>(typeof(TypedTestController), "Head");

                Assert.IsTrue(hasHttpHeadAttribute);
            }
            #endregion HeadHasRightAttributes

            #region Head
            [TestMethod]
            public void Head()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.Head(items[1].Id);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkResult));
            }
            #endregion Head

            #region HeadNotFound
            [TestMethod]
            public void HeadNotFound()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.Head(Guid.NewGuid());

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
            }
            #endregion HeadNotFound

            #region DeleteHasRightAttributes
            [TestMethod]
            public void DeleteHasRightAttributes()
            {
                bool hasHttpDeleteAttribute = MethodHelper.HasAttribute<HttpDeleteAttribute>(typeof(TypedTestController), "Delete");

                Assert.IsTrue(hasHttpDeleteAttribute);
            }
            #endregion DeleteHasRightAttributes

            #region Delete
            [TestMethod]
            public void Delete()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.Delete(items[1].Id);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.OkResult));
            }
            #endregion Delete

            #region DeleteNotFound
            [TestMethod]
            public void DeleteNotFound()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);

                var result = controller.Delete(Guid.NewGuid());

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
            }
            #endregion DeleteNotFound

            #region PostHasRightAttributes
            [TestMethod]
            public void PostHasRightAttributes()
            {
                bool hasHttpPostAttribute = MethodHelper.HasAttribute<HttpPostAttribute>(typeof(TypedTestController), "Post");

                Assert.IsTrue(hasHttpPostAttribute);
            }
            #endregion PostHasRightAttributes

            #region Post
            [TestMethod]
            public void Post()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);
                ValueObject<string> newItem = new ValueObject<string>("SomeOtherString");

                var result = controller.Post(newItem);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.CreatedAtActionResult));
            }
            #endregion Post

            #region PutHasRightAttributes
            [TestMethod]
            public void PutHasRightAttributes()
            {
                bool hasHttpPutAttribute = MethodHelper.HasAttribute<HttpPutAttribute>(typeof(TypedTestController), "Put");

                Assert.IsTrue(hasHttpPutAttribute);
            }
            #endregion PutHasRightAttributes

            #region Put
            [TestMethod]
            public void Put()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);
                ValueObject<string> contentToUpdate = new ValueObject<string>("SomeOtherString");

                var result = controller.Put(items[1].Id, contentToUpdate);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.AcceptedAtActionResult));
            }
            #endregion Put

            #region PutNotFound
            [TestMethod]
            public void PutNotFound()
            {
                List<ValueObject<string>> items = this.generateTestItems();
                TypedTestController controller = new TypedTestController(items);
                ValueObject<string> contentToUpdate = new ValueObject<string>("SomeOtherString");

                var result = controller.Put(Guid.NewGuid(), contentToUpdate);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
            }
            #endregion PutNotFound
        }
        #endregion TypedBaseControllerMethods
    }
}
