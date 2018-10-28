using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class Size : ISize
    {
        #region Properties

        public float Width { get; set; }

        public float Height { get; set; }

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
            this.Width = 0f;
            this.Height = 0f;
        }

        public Size(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public Size(float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        #endregion Construction

        #region Methods

        #region Half
        public ISize Half()
        {
            return new Size(this.Width / 2f, this.Height / 2f);
        }
        #endregion Half

        #region ToString
        public override string ToString()
        {
            return string.Format("{0:##.#} / {1:##.#} @ {2:##.###}", this.Width, this.Height, this.AspectRatio);
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
                result.Width = float.Parse(parts[0]);
                result.Height = float.Parse(parts[1]);
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
