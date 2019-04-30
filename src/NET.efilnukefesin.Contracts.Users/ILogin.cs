using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Users
{
    public interface ILogin
    {
        ICollection<IAlias> Aliases { get; set; }
        string PasswordHash { get; set; }
    }
}
