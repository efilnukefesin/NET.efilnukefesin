using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.DependencyInjection.Assets.Services
{
    class DemoFileDataService : BaseObject, IDataService
    {
        #region Properties

        public string BasePath { get; set; }

        #endregion Properties

        #region Construction

        public DemoFileDataService(string basePath)
        {
            BasePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            
        }
        #endregion dispose

        #endregion Methods
    }
}
