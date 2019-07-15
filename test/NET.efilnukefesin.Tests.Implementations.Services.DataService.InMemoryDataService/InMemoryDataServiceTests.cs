using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.InMemoryDataService
{
    [TestClass]
    public class InMemoryDataServiceTests : BaseSimpleTest
    {
        #region InMemoryDataServiceProperties
        [TestClass]
        public class InMemoryDataServiceProperties : InMemoryDataServiceTests
        {

        }
        #endregion InMemoryDataServiceProperties

        #region InMemoryDataServiceConstruction
        [TestClass]
        public class InMemoryDataServiceConstruction : InMemoryDataServiceTests
        {

        }
        #endregion InMemoryDataServiceConstruction

        #region InMemoryDataServiceMethods
        [TestClass]
        public class InMemoryDataServiceMethods : InMemoryDataServiceTests
        {
            //TODO: add CRUD tests
            #region GetAllAsync
            [TestMethod]
            public void GetAllAsync()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();
                IDataService dataService = DiHelper.GetService<IDataService>();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString1")).GetAwaiter().GetResult();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString2")).GetAwaiter().GetResult();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString3")).GetAwaiter().GetResult();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString4")).GetAwaiter().GetResult();

                var result = dataService.GetAllAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action").GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual(4, result.Count());
            }
            #endregion GetAllAsync

            #region CreateOrUpdateAsyncAppend
            [TestMethod]
            public void CreateOrUpdateAsyncAppend()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest1Action", new ValueObject<string>("TestString")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncAppend
        }
        #endregion InMemoryDataServiceMethods
    }
}
