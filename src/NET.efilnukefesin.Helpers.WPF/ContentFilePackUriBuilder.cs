using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers.WPF
{
    public class ContentFilePackUriBuilder : PackUriBuilder
    {
        #region Properties

        #region AbsolutePath
        public override string AbsolutePath
        {
            get
            {
                return $"{this.Scheme}://application:,,,{this.RelativePath}";
            }
        }
        #endregion AbsolutePath

        #endregion Properties

        #region Construction

        public ContentFilePackUriBuilder(string path)  : base(path) { }

        #endregion Construction        
    }
}
