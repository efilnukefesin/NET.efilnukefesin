using NET.efilnukefesin.Implementations.Base;
using NET.efilnukefesin.Implementations.Rest.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Implementations.Rest.Server.Assets
{
    internal class TypedTestController : TypedBaseController<ValueObject<string>>
    {
        #region Properties

        #endregion Properties

        #region Construction

        public TypedTestController() : base()
        { }

        public TypedTestController(IEnumerable<ValueObject<string>> initialItems) : base(initialItems)
        { }

        #endregion Construction

        #region Methods

        #endregion Methods

        #region Events

        #endregion Events
    }
}
