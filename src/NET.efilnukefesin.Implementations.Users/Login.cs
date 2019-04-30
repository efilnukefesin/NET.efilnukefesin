using NET.efilnukefesin.Contracts.Users;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Users
{
    public class Login : BaseObject, ILogin
    {
        #region Properties
        public ICollection<IAlias> Aliases { get; set; }
        public string PasswordHash { get; set; }

        #endregion Properties

        #region Construction

        public Login(IAlias Alias, string PasswordHash)
        {
            this.Aliases = (ICollection<IAlias>)new List<Alias>();
            this.Aliases.Add(Alias);
            this.PasswordHash = PasswordHash;
        }

        public Login(ICollection<IAlias> Aliases, string PasswordHash)
        {
            this.Aliases = Aliases;
            this.PasswordHash = PasswordHash;
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
