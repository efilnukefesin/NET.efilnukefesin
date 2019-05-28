using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.PlainTextFilesDataService
{
    [TestClass]
    public class PlainTextFilesDataServiceTests : BaseSimpleTest
    {
        protected string testPath = "/Content/Testfiles";

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
                DiSetup.PlainTextFilesDataServiceTests();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                Assert.IsNotNull(dataService);
                Assert.IsInstanceOfType(dataService, typeof(NET.efilnukefesin.Implementations.Services.DataService.PlainTextFilesDataService.PlainTextFilesDataService));
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
                DiSetup.PlainTextFilesDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.GetAsync<bool>("SomeAction").GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion GetAsync

            #region CreateOrUpdateAsync
            [TestMethod]
            public void CreateOrUpdateAsync()
            {
                DiSetup.PlainTextFilesDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.CreateOrUpdateAsync<bool>("SomeOtherAction", true).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsync
        }
        #endregion DataServiceMethods
    }
}
