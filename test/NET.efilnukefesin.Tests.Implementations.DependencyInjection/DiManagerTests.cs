﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.DependencyInjection;
using NET.efilnukefesin.Contracts.DependencyInjection.Classes;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;

namespace NET.efilnukefesin.Tests.Implementations.DiManager
{
    [TestClass]
    public class DiManagerTests : BaseSimpleTest
    {
        #region DiManagerProperties
        [TestClass]
        public class DiManagerProperties : DiManagerTests
        {

        }
        #endregion DiManagerProperties

        #region DiManagerConstruction
        [TestClass]
        public class DiManagerConstruction : DiManagerTests
        {
            #region Construct
            [TestMethod]
            public void Construct()
            {
                IDependencyManager result = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance();

                Assert.IsNotNull(result);
            }
            #endregion Construct
        }
        #endregion DiManagerConstruction

        #region DiManagerMethods
        [TestClass]
        public class DiManagerMethods : DiManagerTests
        {
            #region Resolve
            [TestMethod]
            public void Resolve()
            {
                var result = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<SimpleClass>();

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(SimpleClass));
            }
            #endregion Resolve

            #region RegisterTarget
            [TestMethod]
            public void RegisterTarget()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRegularParameterlessService, RegularParameterlessService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<ClassA>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITestService), new TestService("abc")) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<ClassB>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITestService), new TestService("xyz")) });

                var classA = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassA>();
                var classB = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassB>();

                Assert.IsNotNull(classA);
                Assert.IsNotNull(classB);
                Assert.IsNotNull(classA.Service);
                Assert.IsNotNull(classB.Service);
                Assert.AreEqual("abc", classA.Service.SomeString);
                Assert.AreEqual("xyz", classB.Service.SomeString);
            }
            #endregion RegisterTarget

            #region RegisterTargetWithUri
            [TestMethod]
            public void RegisterTargetWithUri()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRegularParameterlessService, RegularParameterlessService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITestService, TestServiceWithUri>();
                //NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<ClassA>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITestService), new TestServiceWithUri(new Uri("http://google.com"), "abc")) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<ClassA>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(ITestService), new Uri("http://google.com"), "abc") });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<ClassB>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITestService), new TestServiceWithUri(new Uri("http://google.de"), "xyz")) });

                var classA = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassA>();
                var classB = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassB>();

                Assert.IsNotNull(classA);
                Assert.IsNotNull(classB);
                Assert.IsNotNull(classA.Service);
                Assert.IsNotNull(classB.Service);
                Assert.AreEqual("abc", classA.Service.SomeString);
                Assert.AreEqual("xyz", classB.Service.SomeString);
            }
            #endregion RegisterTargetWithUri

            #region PrimitiveParameterInjection
            [TestMethod]
            public void PrimitiveParameterInjection()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRegularParameterlessService, RegularParameterlessService>();

                var classC = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassC>(666, "TheString");

                Assert.IsNotNull(classC);
                Assert.IsNotNull(classC.Service);
                Assert.AreEqual(666, classC.TheNumber);
                Assert.AreEqual("TheString", classC.SomeString);
            }
            #endregion PrimitiveParameterInjection

            #region ComplexParameterInjection
            [TestMethod]
            public void ComplexParameterInjection()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRegularParameterlessService, RegularParameterlessService>();

                var classD = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassD>(666, "TheString", new ComplexClass("AnotherText", 999.9999));

                Assert.IsNotNull(classD);
                Assert.IsNotNull(classD.Service);
                Assert.IsNotNull(classD.Complex);
                Assert.AreEqual(666, classD.TheNumber);
                Assert.AreEqual("TheString", classD.SomeString);
                Assert.AreEqual("AnotherText", classD.Complex.TheText);
                Assert.AreEqual(999.9999, classD.Complex.TheNumber);
            }
            #endregion ComplexParameterInjection

            #region HttpMessageHandlerParameterInjection
            [TestMethod]
            public void HttpMessageHandlerParameterInjection()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRegularParameterlessService, RegularParameterlessService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().AddTypeTranslation("HttpMessageHandlerProxy", typeof(HttpMessageHandler));
                //NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().AddTypeTranslation("Castle.Proxies.HttpMessageHandlerProxy", typeof(HttpMessageHandler));

                var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                handlerMock
                   .Protected()
                   // Setup the PROTECTED method to mock
                   .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                   )
                   // prepare the expected response of the mocked http call
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent("Hello World!"),
                   })
                   .Verifiable();

                var classE = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<ClassE>(666, "TheString", new ComplexClass("AnotherText", 999.9999), handlerMock.Object);

                Assert.IsNotNull(classE);
                Assert.IsNotNull(classE.Service);
                Assert.IsNotNull(classE.Complex);
                Assert.IsNotNull(classE.Handler);
                Assert.AreEqual(666, classE.TheNumber);
                Assert.AreEqual("TheString", classE.SomeString);
                Assert.AreEqual("AnotherText", classE.Complex.TheText);
                Assert.AreEqual(999.9999, classE.Complex.TheNumber);
            }
            #endregion HttpMessageHandlerParameterInjection

            #region TwoServicesForOneInterfaceWithOneParameter
            [TestMethod]
            public void TwoServicesForOneInterfaceWithOneParameter()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceService, TwoServicesForOneInterfaceService1>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceService, TwoServicesForOneInterfaceService2>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<TwoServicesForOneInterfaceTarget1>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITwoServicesForOneInterfaceService), new TwoServicesForOneInterfaceService1(666)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<TwoServicesForOneInterfaceTarget2>(new List<TypeInstanceParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITwoServicesForOneInterfaceService), new TwoServicesForOneInterfaceService2("xyz")) });

                var a = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<TwoServicesForOneInterfaceTarget1>();
                var b = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<TwoServicesForOneInterfaceTarget2>();

                Assert.IsNotNull(a);
                Assert.IsNotNull(b);
                Assert.IsInstanceOfType(a.Service, typeof(TwoServicesForOneInterfaceService1));
                Assert.IsInstanceOfType(b.Service, typeof(TwoServicesForOneInterfaceService2));
            }
            #endregion TwoServicesForOneInterfaceWithOneParameter

            #region TwoServicesForOneInterfaceWithTwoParameters
            [TestMethod]
            public void TwoServicesForOneInterfaceWithTwoParameters()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceService, TwoServicesForOneInterfaceService1>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceService, TwoServicesForOneInterfaceService2>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceOtherService, TwoServicesForOneInterfaceOtherService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<TwoServicesForOneInterfaceTarget3>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITwoServicesForOneInterfaceService), new TwoServicesForOneInterfaceService1(666)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<TwoServicesForOneInterfaceTarget4>(new List<ParameterInfoObject>() { new TypeInstanceParameterInfoObject(typeof(ITwoServicesForOneInterfaceService), new TwoServicesForOneInterfaceService2("xyz")) });

                var a = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<TwoServicesForOneInterfaceTarget3>();
                var b = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<TwoServicesForOneInterfaceTarget4>();

                Assert.IsNotNull(a);
                Assert.IsNotNull(b);
                Assert.IsInstanceOfType(a.Service, typeof(TwoServicesForOneInterfaceService1));
                Assert.IsInstanceOfType(b.Service, typeof(TwoServicesForOneInterfaceService2));
                Assert.IsInstanceOfType(a.Service2, typeof(TwoServicesForOneInterfaceOtherService));
                Assert.IsInstanceOfType(b.Service2, typeof(TwoServicesForOneInterfaceOtherService));
            }
            #endregion TwoServicesForOneInterfaceWithTwoParameters

            #region TwoServicesForOneInterfaceWithTwoParametersDynamically
            [TestMethod]
            public void TwoServicesForOneInterfaceWithTwoParametersDynamically()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceService, TwoServicesForOneInterfaceService3>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceService, TwoServicesForOneInterfaceService4>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ITwoServicesForOneInterfaceOtherService, TwoServicesForOneInterfaceOtherService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<TwoServicesForOneInterfaceTarget3>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(ITwoServicesForOneInterfaceService), typeof(TwoServicesForOneInterfaceService3), 666) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<TwoServicesForOneInterfaceTarget4>(new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(ITwoServicesForOneInterfaceService), typeof(TwoServicesForOneInterfaceService4), "xyz") });

                var a = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<TwoServicesForOneInterfaceTarget3>();
                var b = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<TwoServicesForOneInterfaceTarget4>();

                Assert.IsNotNull(a);
                Assert.IsNotNull(b);
                Assert.IsInstanceOfType(a.Service, typeof(TwoServicesForOneInterfaceService3));
                Assert.IsInstanceOfType(b.Service, typeof(TwoServicesForOneInterfaceService4));
                Assert.IsInstanceOfType(a.Service2, typeof(TwoServicesForOneInterfaceOtherService));
                Assert.IsInstanceOfType(b.Service2, typeof(TwoServicesForOneInterfaceOtherService));
            }
            #endregion TwoServicesForOneInterfaceWithTwoParametersDynamically

            #region ComplexResolving
            [TestMethod]
            public void ComplexResolving()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IDataService, DemoFileDataService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IDataService, DemoSomeDataService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IUserService, DemoUserService>(Lifetime.Singleton);
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRoleService, DemoRoleService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IPermissionService, DemoPermissionService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoUserService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService), "BasePath"), new DynamicParameterInfoObject(typeof(IRoleService), typeof(DemoRoleService)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoRoleService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService), "BasePath"), new DynamicParameterInfoObject(typeof(IPermissionService), typeof(DemoPermissionService)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoPermissionService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService), "BasePath") });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoAuthenticationService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService), "BasePath"), new DynamicParameterInfoObject(typeof(IUserService), typeof(DemoUserService)), new DynamicParameterInfoObject(typeof(IPermissionService), typeof(DemoPermissionService)), new DynamicParameterInfoObject(typeof(IRoleService), typeof(DemoRoleService)) });

                var userService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoUserService>();
                var permissionService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoPermissionService>();
                var roleService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoRoleService>();
                var authenticationService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoAuthenticationService>();

                Assert.IsNotNull(authenticationService);
                Assert.IsNotNull(userService);
                Assert.IsNotNull(permissionService);
                Assert.IsNotNull(roleService);
                Assert.IsInstanceOfType(authenticationService.UserService.DataService, typeof(DemoFileDataService));
                Assert.AreEqual("BasePath", (authenticationService.UserService.DataService as DemoFileDataService).BasePath);
            }
            #endregion ComplexResolving

            #region ComplexerResolving
            [TestMethod]
            public void ComplexerResolving()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IDataService, DemoFileDataService2>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IDataService, DemoSomeDataService2>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ILogger, DemoLogger>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IUserService, DemoUserService>(Lifetime.Singleton);
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IRoleService, DemoRoleService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IPermissionService, DemoPermissionService>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoUserService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath"), new DynamicParameterInfoObject(typeof(IRoleService), typeof(DemoRoleService)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoRoleService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath"), new DynamicParameterInfoObject(typeof(IPermissionService), typeof(DemoPermissionService)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoPermissionService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath") });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoAuthenticationService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath"), new DynamicParameterInfoObject(typeof(IUserService), typeof(DemoUserService)), new DynamicParameterInfoObject(typeof(IPermissionService), typeof(DemoPermissionService)), new DynamicParameterInfoObject(typeof(IRoleService), typeof(DemoRoleService)) });

                var userService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoUserService>();
                var permissionService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoPermissionService>();
                var roleService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoRoleService>();
                var authenticationService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoAuthenticationService>();

                Assert.IsNotNull(authenticationService);
                Assert.IsNotNull(userService);
                Assert.IsNotNull(permissionService);
                Assert.IsNotNull(roleService);
                Assert.IsInstanceOfType(authenticationService.UserService.DataService, typeof(DemoFileDataService2));
                Assert.AreEqual("BasePath", (authenticationService.UserService.DataService as DemoFileDataService2).BasePath);
            }
            #endregion ComplexerResolving

            #region ComplexerResolvingViaInterface
            [TestMethod]
            public void ComplexerResolvingViaInterface()
            {
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Reset();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IDataService, DemoFileDataService2>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<IDataService, DemoSomeDataService2>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterType<ILogger, DemoLogger>();
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<IUserService, DemoUserService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath"), new DynamicParameterInfoObject(typeof(IRoleService), typeof(DemoRoleService)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<IRoleService, DemoRoleService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath"), new DynamicParameterInfoObject(typeof(IPermissionService), typeof(DemoPermissionService)) });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<IPermissionService, DemoPermissionService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath") });
                NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().RegisterTarget<DemoAuthenticationService>(Lifetime.Singleton, new List<ParameterInfoObject>() { new DynamicParameterInfoObject(typeof(IDataService), typeof(DemoFileDataService2), "BasePath"), new DynamicParameterInfoObject(typeof(IUserService), typeof(DemoUserService)), new DynamicParameterInfoObject(typeof(IPermissionService), typeof(DemoPermissionService)), new DynamicParameterInfoObject(typeof(IRoleService), typeof(DemoRoleService)) });

                //TODO: tell dimanager that if you resolve that target (as it's registered) using the interface, then use stuff I told you above.

                var userService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<IUserService>();
                var permissionService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<IPermissionService>();
                var roleService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<IRoleService>();
                var authenticationService = NET.efilnukefesin.Implementations.DependencyInjection.DiManager.GetInstance().Resolve<DemoAuthenticationService>();

                Assert.IsNotNull(authenticationService);
                Assert.IsNotNull(userService);
                Assert.IsNotNull(permissionService);
                Assert.IsNotNull(roleService);
                Assert.IsInstanceOfType(authenticationService.UserService.DataService, typeof(DemoFileDataService2));
                Assert.AreEqual("BasePath", (authenticationService.UserService.DataService as DemoFileDataService2).BasePath);
            }
            #endregion ComplexerResolvingViaInterface
        }
        #endregion DiManagerMethods
    }
}
