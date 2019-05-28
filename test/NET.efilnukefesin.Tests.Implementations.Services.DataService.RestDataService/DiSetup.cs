using NET.efilnukefesin.Contracts.Services.DataService;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Implementations.Services.DataService.RestDataService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Services.DataService.RestDataService
{
    public static class DiSetup
    {
        #region Methods

        #region Tests
        public static void Tests()
        {
            DiSetup.@base();
            DiManager.GetInstance().AddTypeTranslation("HttpMessageHandlerProxy", typeof(HttpMessageHandler));
        }
        #endregion Tests


        #region base
        private static void @base()
        {
            DiManager.GetInstance().RegisterType<IEndpointRegister, NET.efilnukefesin.Implementations.Services.DataService.EndpointRegister.EndpointRegister>(NET.efilnukefesin.Contracts.DependencyInjection.Enums.Lifetime.Singleton);  //where is all the data coming from?
            DiManager.GetInstance().RegisterType<IDataService, NET.efilnukefesin.Implementations.Services.DataService.RestDataService.RestDataService>();  //where is all the data coming from?

            //DiManager.GetInstance().RegisterTarget<PermissionServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6008")) });
            //DiManager.GetInstance().RegisterTarget<SuperHotFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6010")) });
            //DiManager.GetInstance().RegisterTarget<SuperHotOtherFeatureServer.SDK.Client>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), new Uri("http://localhost:6012")) });
            //TODO: use config values
        }
        #endregion base

        #region Initialize
        //TODO: migrate later on somewhere else, when making a generic Bootstrapper
        public static void Initialize()
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
            }
        }
        #endregion Initialize

        #endregion Methods
    }
}
