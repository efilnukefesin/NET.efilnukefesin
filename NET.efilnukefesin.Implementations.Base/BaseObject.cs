using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public abstract class BaseObject : BaseDisposable, IBaseObject
    {
        #region Properties

        #region Id
        private Guid id = Guid.NewGuid();
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        #endregion Id

        #endregion Properties
    }
}
