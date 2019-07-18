using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Implementations.Rest.Client;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;
using NET.efilnukefesin.Tests.Implementations.Rest.Client.Assets;

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

                TypedTestClient client = DiHelper.GetService<TypedTestClient>();

                Assert.IsNotNull(client);
            }
            #endregion Resolve
        }
        #endregion TypedBaseClientConstruction

        #region TypedBaseClientMethods
        [TestClass]
        public class TypedBaseClientMethods : TypedBaseClientTests
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
        #endregion TypedBaseClientMethods
    }
}
