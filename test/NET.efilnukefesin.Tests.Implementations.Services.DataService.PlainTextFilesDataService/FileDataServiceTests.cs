using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.FileDataService
{
    [TestClass]
    public class FileDataServiceTests : BaseSimpleTest
    {
        protected string testPath = "/Content/Testfiles";

        #region FileDataServiceProperties
        [TestClass]
        public class FileDataServiceProperties : FileDataServiceTests
        {

        }
        #endregion FileDataServiceProperties

        #region FileDataServiceConstruction
        [TestClass]
        public class FileDataServiceConstruction : FileDataServiceTests
        {
            #region IsNotNull
            [TestMethod]
            public void IsNotNull()
            {
                DiSetup.FileDataServiceTests();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                Assert.IsNotNull(dataService);
                Assert.IsInstanceOfType(dataService, typeof(NET.efilnukefesin.Implementations.Services.DataService.FileDataService.FileDataService));
            }
            #endregion IsNotNull
        }
        #endregion FileDataServiceConstruction

        #region FileDataServiceMethods
        [TestClass]
        public class FileDataServiceMethods : FileDataServiceTests
        {
            #region GetAsyncInt
            [TestMethod]
            public void GetAsyncInt()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                var result = dataService.GetAsync<int>("GetAsyncTest1Action").GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(123, result);
            }
            #endregion GetAsyncInt

            //TODO: add more complex objects and lists here, continue with TextFile2 and 3

            #region CreateOrUpdateAsync
            [TestMethod]
            public void CreateOrUpdateAsync()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.CreateOrUpdateAsync<string>("CreateOrUpdateAsyncTest1Action", "TestString").GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsync

            #region DeleteAsync
            [TestMethod]
            public void DeleteAsync()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.DeleteAsync<bool>("SomeOtherAction", true).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsync
        }
        #endregion FileDataServiceMethods
    }
}
