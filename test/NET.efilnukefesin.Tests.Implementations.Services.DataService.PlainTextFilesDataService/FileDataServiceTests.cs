using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
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

                var result = dataService.GetAsync<ValueObject<int>>("GetAsyncTest1Action").GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(123, result.Value);
            }
            #endregion GetAsyncInt

            //TODO: add more complex objects and lists here, continue with TextFile2 and 3

            #region CreateOrUpdateAsyncAppend
            [TestMethod]
            public void CreateOrUpdateAsyncAppend()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest1Action", new ValueObject<string>("TestString")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncAppend

            #region CreateOrUpdateAsyncUpdate
            [TestMethod]
            public void CreateOrUpdateAsyncUpdate()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("Opel")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncUpdate

            #region CreateOrUpdateAsyncCreateNew
            [TestMethod]
            public void CreateOrUpdateAsyncCreateNew()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", new ValueObject<string>("TestString")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncCreateNew

            #region CreateOrUpdateAsyncCreateNewWithLists
            [TestMethod]
            public void CreateOrUpdateAsyncCreateNewWithLists()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);
                List<ValueObject<string>> stringList = new List<ValueObject<string>>() { new ValueObject<string>("a"), new ValueObject<string>("b"), new ValueObject<string>("c") };

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest4Action", stringList).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncCreateNewWithLists

            #region DeleteAsync
            [TestMethod]
            public void DeleteAsync()
            {
                DiSetup.FileDataServiceTests();
                DiSetup.InitializeFileEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>(this.testPath);

                bool result = dataService.DeleteAsync<ValueObject<string>>("DeleteAsyncTest1Action", "Opel").GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsync
        }
        #endregion FileDataServiceMethods
    }
}
