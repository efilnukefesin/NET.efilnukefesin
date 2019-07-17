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

            #region GetAsync
            [TestMethod]
            public void GetAsync()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();
                IDataService dataService = DiHelper.GetService<IDataService>();
                ValueObject<string> testObject2 = new ValueObject<string>("TestString2");
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString1")).GetAwaiter().GetResult();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", testObject2).GetAwaiter().GetResult();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString3")).GetAwaiter().GetResult();
                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", new ValueObject<string>("TestString4")).GetAwaiter().GetResult();

                var result = dataService.GetAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", testObject2.Id).GetAwaiter().GetResult();

                Assert.IsNotNull(result);
                Assert.AreEqual("TestString2", result.Value);
            }
            #endregion GetAsync

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

            #region CreateOrUpdateAsyncList
            [TestMethod]
            public void CreateOrUpdateAsyncList()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();
                List<ValueObject<string>> items = new List<ValueObject<string>>();
                items.Add(new ValueObject<string>("TestString1"));
                items.Add(new ValueObject<string>("TestString2"));
                items.Add(new ValueObject<string>("TestString3"));
                items.Add(new ValueObject<string>("TestString4"));

                bool result = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest2Action", items).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion CreateOrUpdateAsyncList

            #region DeleteAsync
            [TestMethod]
            public void DeleteAsync()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();
                List<ValueObject<string>> items = new List<ValueObject<string>>();
                items.Add(new ValueObject<string>("TestString1"));
                items.Add(new ValueObject<string>("TestString2"));
                items.Add(new ValueObject<string>("TestString3"));
                items.Add(new ValueObject<string>("TestString4"));

                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", items).GetAwaiter().GetResult();
                bool result = dataService.DeleteAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", items[1].Id).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsync

            #region CreateOrUpdateAsyncWithDelegate
            [TestMethod]
            public void CreateOrUpdateAsyncWithDelegate()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();
                List<ValueObject<string>> items = new List<ValueObject<string>>();
                items.Add(new ValueObject<string>("TestString1"));
                items.Add(new ValueObject<string>("TestString2"));
                items.Add(new ValueObject<string>("TestString3"));
                items.Add(new ValueObject<string>("TestString4"));

                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", items).GetAwaiter().GetResult();

                bool resultAdd = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", new ValueObject<string>("TestString"), x => x.Value.Equals("DunnoYet")).GetAwaiter().GetResult();
                bool resultUpdate = dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", new ValueObject<string>("TestString"), x => x.Value.Equals("TestString4")).GetAwaiter().GetResult();

                int numberafterwards = dataService.GetAllAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action").GetAwaiter().GetResult().Count();

                Assert.AreEqual(true, resultAdd);
                Assert.AreEqual(true, resultUpdate);
                Assert.AreEqual(5, numberafterwards);
            }
            #endregion CreateOrUpdateAsyncWithDelegate

            #region DeleteAsyncWithDelegate
            [TestMethod]
            public void DeleteAsyncWithDelegate()
            {
                DiSetup.InMemoryDataServiceTests();
                DiSetup.InitializeInMemoryEndpoints();

                IDataService dataService = DiHelper.GetService<IDataService>();
                List<ValueObject<string>> items = new List<ValueObject<string>>();
                items.Add(new ValueObject<string>("TestString1"));
                items.Add(new ValueObject<string>("TestString2"));
                items.Add(new ValueObject<string>("TestString3"));
                items.Add(new ValueObject<string>("TestString4"));

                dataService.CreateOrUpdateAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", items).GetAwaiter().GetResult();
                bool result = dataService.DeleteAsync<ValueObject<string>>("CreateOrUpdateAsyncTest3Action", x => x.Value.Equals("TestString3")).GetAwaiter().GetResult();

                Assert.AreEqual(true, result);
            }
            #endregion DeleteAsyncWithDelegate
        }
        #endregion InMemoryDataServiceMethods
    }
}
