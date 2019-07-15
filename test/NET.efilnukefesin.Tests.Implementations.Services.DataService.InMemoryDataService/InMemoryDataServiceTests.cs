using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.BootStrapper;
using System;
using System.Collections.Generic;
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
            #region CreateOrUpdateAsyncAppend
            [TestMethod]
            public void CreateOrUpdateAsyncAppend()
            {
                DiSetup.InMemoryDataServiceTests();

                IDataService dataService = DiHelper.GetService<IDataService>();

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest1Action", new ValueObject<string>("TestString")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncAppend
        }
        #endregion InMemoryDataServiceMethods
    }
}
