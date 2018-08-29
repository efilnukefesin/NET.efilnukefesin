using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class Size : ISize
    {
        #region Properties

        #region Width
        private int width;
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }
        #endregion Width

        #region Height
        private int height;
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        #endregion Height

        #region AspectRatio
        public float AspectRatio
        {
            get { return (float)this.width / (float)this.height; }
        }
        #endregion AspectRatio

        #endregion Properties

        #region Construction

        public Size()
        {
            this.width = 0;
            this.height = 0;
        }

        public Size(int Width, int Height)
        {
            this.width = Width;
            this.height = Height;
        }

        #endregion Construction

        #region Methods

        #region ToString
        public override string ToString()
        {
            return string.Format("{0} / {1} @ {2:##.###}", this.width, this.height, this.AspectRatio);
        }
        #endregion ToString

        #endregion Methods

        #region Static Methods

        #region Parse
        public static Size Parse(string input)
        {
            Size result = new Size();
            try
            {
                string[] parts = input.Split('/', '@');
                result.width = int.Parse(parts[0]);
                result.height = int.Parse(parts[1]);
            }
            catch (Exception ex)
            {
                throw new FormatException("error while parsing - string seperated by '/'?", ex);
            }
            return result;
        }
        #endregion Parse

        #endregion Static Methods
    }
}
