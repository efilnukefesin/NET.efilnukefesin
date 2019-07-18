using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Tests.Implementations.Rest.Server.Assets;
using System;
using System.Collections.Generic;
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
        }
        #endregion TypedBaseControllerConstruction

        #region TypedBaseControllerMethods
        [TestClass]
        public class TypedBaseControllerMethods : TypedBaseControllerTests
        {
            #region GetAll
            [TestMethod]
            public void GetAll()
            {
                throw new NotImplementedException();
            }
            #endregion GetAll

            #region Get
            [TestMethod]
            public void Get()
            {
                throw new NotImplementedException();
            }
            #endregion Get

            #region Delete
            [TestMethod]
            public void Delete()
            {
                throw new NotImplementedException();
            }
            #endregion Delete

            #region Exists
            [TestMethod]
            public void Exists()
            {
                throw new NotImplementedException();
            }
            #endregion Exists

            #region Create
            [TestMethod]
            public void Create()
            {
                throw new NotImplementedException();
            }
            #endregion Create

            #region Update
            [TestMethod]
            public void Update()
            {
                throw new NotImplementedException();
            }
            #endregion Update
        }
        #endregion TypedBaseControllerMethods
    }
}
