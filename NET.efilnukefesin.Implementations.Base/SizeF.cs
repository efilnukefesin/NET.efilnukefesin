using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    public class SizeF : ISizeF
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

        public SizeF()
        {
            this.Width = 0f;
            this.Height = 0f;
        }

        public SizeF(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        public SizeF(float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
        }

        #endregion Construction

        #region Methods

        #region Half
        public ISizeF Half()
        {
            return new SizeF(this.Width / 2f, this.Height / 2f);
        }
        #endregion Half

        #region ToString
        public override string ToString()
        {
            return $"{this.Width:##.#} / {this.Height:##.#} @ {this.AspectRatio:##.###}";
        }
        #endregion ToString

        #endregion Methods

        #region Static Methods

        #region Parse
        public static SizeF Parse(string input)
        {
            SizeF result = new SizeF();
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
