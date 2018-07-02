using NET.efilnukefesin.Contracts.Users;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NET.efilnukefesin.Implementations.Users
{
    public class User : BaseObject, IUser
    {
        #region Properties
        public ICollection<ILogin> Logins { get; set; }

        [Obsolete]
        public string Username { get; set; }
        public Image Image { get; set; }
        public string UserDisplayname { get; set; }

        #endregion Properties

        #region Construction

        public User()
        {
            this.Username = Environment.UserName;
            try
            {
                this.UserDisplayname = UserPrincipal.Current.DisplayName;
            }
            catch (Exception ex)
            {
                
            }
            this.Image = this.getUserimage(this.Username);
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

        #region getUserimage
        private Image getUserimage(string username)
        {
            Image result = default(Image);
            result = Image.FromFile(getUserTilePath(username));
            return result;
        }
        #endregion getUserimage

        #region getUserTilePath
        [DllImport("shell32.dll", EntryPoint = "#261", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void getUserTilePath(string username, UInt32 whatever, /* 0x80000000 */ StringBuilder picpath, int maxLength);
        private static string getUserTilePath(string username)
        {   // username: use null for current user
            var sb = new StringBuilder(1000);
            User.getUserTilePath(username, 0x80000000, sb, sb.Capacity);
            return sb.ToString();
        }
        #endregion getUserTilePath
    }
}
