using NET.efilnukefesin.Contracts.Users;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Users
{
    public class Alias : BaseObject, IAlias
    {
        #region Properties

        public string Name { get; set; }

        #endregion Properties

        #region Construction

        public Alias(string Name)
        {
            this.Name = Name;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            throw new NotImplementedException();
        }
        #endregion dispose

        #endregion Methods
    }
}
