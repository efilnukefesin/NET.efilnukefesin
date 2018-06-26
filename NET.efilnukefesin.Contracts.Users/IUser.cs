using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.Users
{
    public interface IUser
    {
        ICollection<ILogin> Logins { get; set; }
    }
}
