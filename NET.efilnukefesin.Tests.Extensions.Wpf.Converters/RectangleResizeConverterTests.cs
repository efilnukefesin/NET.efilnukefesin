using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Extensions.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Tests.Extensions.Wpf.Converters
{
    [TestClass]
    public class RectangleResizeConverterTests : BaseSimpleTest
    {
        #region RectangleResizeConverterProperties
        [TestClass]
        public class RectangleResizeConverterProperties : RectangleResizeConverterTests
        {

        }
        #endregion RectangleResizeConverterProperties

        #region RectangleResizeConverterConstruction
        [TestClass]
        public class RectangleResizeConverterConstruction : RectangleResizeConverterTests
        {

        }
        #endregion RectangleResizeConverterConstruction

        #region RectangleResizeConverterMethods
        [TestClass]
        public class RectangleResizeConverterMethods : RectangleResizeConverterTests
        {
            #region Convert
            [TestMethod]
            public void Convert()
            {
                RectangleResizeConverter rectangleResizeConverter = new RectangleResizeConverter();
                throw new NotImplementedException();
            }
            #endregion Convert

            #region ConvertBack
            [TestMethod]
            public void ConvertBack()
            {
                RectangleResizeConverter rectangleResizeConverter = new RectangleResizeConverter();
                throw new NotImplementedException();
            }
            #endregion ConvertBack
        }
        #endregion RectangleResizeConverterMethods
    }

}
