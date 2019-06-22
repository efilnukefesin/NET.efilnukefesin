using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Contracts.Mvvm;
using NET.efilnukefesin.Implementations.DependencyInjection;
using NET.efilnukefesin.Tests.BootStrapper;
using NET.efilnukefesin.Tests.Implementations.Mvvm.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Mvvm
{
    [TestClass]
    public class NavigationsServiceTests : BaseSimpleTest
    {
        #region NavigationsServiceProperties
        [TestClass]
        public class NavigationsServiceProperties : NavigationsServiceTests
        {

        }
        #endregion NavigationsServiceProperties

        #region NavigationsServiceConstruction
        [TestClass]
        public class NavigationsServiceConstruction : NavigationsServiceTests
        {
            [TestMethod]
            public void Resolve()
            {
                DiSetup.Tests();
                DiManager.GetInstance().RegisterInstance<INavigationPresenter>(new DummyNavigationPresenter());

                var navigationService = DiHelper.GetService<INavigationService>();

                Assert.IsNotNull(navigationService);
            }
        }
        #endregion NavigationsServiceConstruction

        #region NavigationsServiceMethods
        [TestClass]
        public class NavigationsServiceMethods : NavigationsServiceTests
        {
            
        }
        #endregion NavigationsServiceMethods
    }

}
