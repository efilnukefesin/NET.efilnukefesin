using NET.efilnukefesin.Contracts.Users;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Users
{
    public class User : BaseObject, IUser
    {
        #region Properties
        public ICollection<ILogin> Logins { get; set; }

        #endregion Properties

        #region Construction

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
