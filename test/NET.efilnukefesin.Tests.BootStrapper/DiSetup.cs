using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using NET.efilnukefesin.Contracts.FeatureToggling;
using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.FeatureToggling;
using NET.efilnukefesin.Implementations.Logger.SerilogLogger;
using NET.efilnukefesin.Implementations.Mvvm;
using NET.efilnukefesin.Implementations.Services.DataService.RestDataService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NET.efilnukefesin.Implementations.Services.DataService.InMemoryDataService;

namespace NET.efilnukefesin.Tests.BootStrapper
{
    public static class DiSetup
    {
        #region Methods

        public static void AddToAspNetCore(IServiceCollection services)
        {
            services.AddSingleton<IDataService>(s => DiHelper.GetService<InMemoryDataService>("Data"));
            DiSetup.Initialize();
        }

        #region RestDataServiceTestsTests
        public static void RestDataServiceTests()
        {
            DiSetup.Tests();
            DiManager.GetInstance().RegisterType<IDataService, NET.efilnukefesin.Implementations.Services.DataService.RestDataService.RestDataService>();  //TODO: switch per test
        }
        #endregion RestDataServiceTestsTests

        #region FileDataServiceTests
        public static void FileDataServiceTests()
        {
            DiSetup.Tests();
            DiManager.GetInstance().RegisterType<IDataService, NET.efilnukefesin.Implementations.Services.DataService.FileDataService.FileDataService>();  //TODO: switch per test
        }
        #endregion FileDataServiceTests

        #region InMemoryDataServiceTests
        public static void InMemoryDataServiceTests()
        {
            DiSetup.Tests();
            DiManager.GetInstance().RegisterType<IDataService, NET.efilnukefesin.Implementations.Services.DataService.InMemoryDataService.InMemoryDataService>();  //TODO: switch per test
        }
        #endregion InMemoryDataServiceTests

        #region Tests
        public static void Tests()
        {
            DiSetup.@base();
            DiManager.GetInstance().AddTypeTranslation("HttpMessageHandlerProxy", typeof(HttpMessageHandler));
            DiManager.GetInstance().AddTypeTranslation("Microsoft.AspNetCore.Mvc.Testing.Handlers.RedirectHandler", typeof(HttpMessageHandler));
            DiManager.GetInstance().AddTypeTranslation("RedirectHandler", typeof(HttpMessageHandler));
        }
        #endregion Tests

        #region base
        private static void @base()
        {
            DiManager.GetInstance().RegisterType<IEndpointRegister, NET.efilnukefesin.Implementations.Services.DataService.EndpointRegister.EndpointRegister>(Lifetime.Singleton);  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<IFeatureToggleManager, FeatureToggleManager>(Lifetime.Singleton);
            DiManager.GetInstance().RegisterType<ILogger, SerilogLogger>();
            DiManager.GetInstance().RegisterType<INavigationService, NavigationService>(Lifetime.Singleton);

            //DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6008")) });
            //DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6010")) });
            //DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6012")) });
            //TODO: use config values
        }
        #endregion base

        #region Initialize
        public static void Initialize()
        {

        }
        #endregion Initialize

        #region InitializeRestEndpoints
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void InitializeRestEndpoints()
        {
            IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("SuperHotFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("SuperHotOtherFeatureServer.SDK.Client.GetValueAsync", "api/values");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.AddUserAsync", "api/adduser");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.GetUserAsync", "api/permissions");
                endpointRegister.AddEndpoint("PermissionServer.SDK.Client.CheckPermissionAsync", "api/permissions/check");
                endpointRegister.AddEndpoint("PermissionServer.Client.BaseClient.GetGivenPermissionsAsync", "api/permissions/givenpermissions");
                endpointRegister.AddEndpoint("TestResourceLocation", "api/permissions/");
                endpointRegister.AddEndpoint("ValueStore", "api/values/");
                endpointRegister.AddEndpoint("SpecialValueStore", "api/specialvalues/");
            }
        }
        #endregion InitializeRestEndpoints

        #region InitializeFileEndpoints
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void InitializeFileEndpoints()
        {
            IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("GetAsyncTest1Action", "TextFile1.json");
                endpointRegister.AddEndpoint("GetAsyncTest4Action", "TextFile7.json");
                endpointRegister.AddEndpoint("GetAsyncTest2Action", "TextFile2.json");
                endpointRegister.AddEndpoint("GetAsyncTest3Action", "TextFile3.json");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest1Action", "TextFile4.json");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest2Action", "TextFile5.json");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest3Action", "TextFile55.json");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest4Action", "TextFile555.json");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest5Action", "TextFile5555.json");
                endpointRegister.AddEndpoint("DeleteAsyncTest1Action", "TextFile6.json");
                endpointRegister.AddEndpoint("DeleteAsyncTest2Action", "TextFile8.json");
            }
        }
        #endregion InitializeFileEndpoints

        #region InitializeInMemoryEndpoints
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void InitializeInMemoryEndpoints()
        {
            IEndpointRegister endpointRegister = DiHelper.GetService<IEndpointRegister>();
            if (endpointRegister != null)
            {
                endpointRegister.AddEndpoint("GetAsyncTest1Action", "GetAsyncTest1");
                endpointRegister.AddEndpoint("GetAsyncTest4Action", "GetAsyncTest4");
                endpointRegister.AddEndpoint("GetAsyncTest2Action", "GetAsyncTest2");
                endpointRegister.AddEndpoint("GetAsyncTest3Action", "GetAsyncTest3");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest1Action", "CreateOrUpdateAsyncTest1");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest2Action", "CreateOrUpdateAsyncTest2");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest3Action", "CreateOrUpdateAsyncTest3");
                endpointRegister.AddEndpoint("CreateOrUpdateAsyncTest4Action", "CreateOrUpdateAsyncTest4");
                endpointRegister.AddEndpoint("DeleteAsyncTest1Action", "DeleteAsyncTest1");
            }
        }
        #endregion InitializeInMemoryEndpoints

        #endregion Methods
    }
}
