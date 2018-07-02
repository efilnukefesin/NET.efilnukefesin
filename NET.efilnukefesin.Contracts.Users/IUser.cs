using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace NET.efilnukefesin.Contracts.Users
{
    public interface IUser
    {
        ICollection<ILogin> Logins { get; set; }

        [Obsolete]
        string Username { get; set; }
        [Obsolete]
        string UserDisplayname { get; set; }
        Image Image { get; set; }
    }
}
