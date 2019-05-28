using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.PlainTextFilesDataService
{
    [TestClass]
    public class PlainTextFilesDataServiceTests : BaseSimpleTest
    {
        #region DataServiceProperties
        [TestClass]
        public class PlainTextFilesDataServiceProperties : PlainTextFilesDataServiceTests
        {

        }
        #endregion DataServiceProperties

        #region DataServiceConstruction
        [TestClass]
        public class PlainTextFilesDataServiceConstruction : PlainTextFilesDataServiceTests
        {
            #region IsNotNull
            [TestMethod]
            public void IsNotNull()
            {
                DiSetup.Tests();

                IDataService dataService = DiHelper.GetService<IDataService>("/Assets/Testdata/");

                Assert.IsNotNull(dataService);
            }
            #endregion IsNotNull
        }
        #endregion DataServiceConstruction

        #region DataServiceMethods
        [TestClass]
        public class PlainTextFilesDataServiceMethods : PlainTextFilesDataServiceTests
        {
            #region GetAsync
            [TestMethod]
            public void GetAsync()
            {
                DiSetup.Tests();


                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://baseUri"), "someToken", handlerMock.Object);

                bool result = dataService.GetAsync<bool>("SomeAction").GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion GetAsync

            #region CreateOrUpdateAsync
            [TestMethod]
            public void CreateOrUpdateAsync()
            {
                DiSetup.Tests();

                IDataService dataService = DiHelper.GetService<IDataService>(new Uri("http://localhost"), "someToken25", handlerMock.Object);

                bool result = dataService.CreateOrUpdateAsync<bool>("SomeOtherAction", true).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsync
        }
        #endregion DataServiceMethods
    }
}
