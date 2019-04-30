using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class Size : ISize
    {
        #region Properties

        public int Width { get; set; }

        public int Height { get; set; }

        #region AspectRatio
        public float AspectRatio
        {
            get { return this.Width / this.Height; }
        }
        #endregion AspectRatio

        #endregion Properties

        #region Construction

        public Size()
        {
            this.Width = 0;
            this.Height = 0;
        }

        public Size(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        #endregion Construction

        #region Methods

        #region Half
        public ISize Half()
        {
            return new Size((int)(this.Width / 2f), (int)(this.Height / 2f));
        }
        #endregion Half

        #region ToString
        public override string ToString()
        {
            return $"{this.Width} / {this.Height} @ {this.AspectRatio:##.###}";
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
                result.Width = int.Parse(parts[0]);
                result.Height = int.Parse(parts[1]);
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
