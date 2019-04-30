using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Extensions.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NET.efilnukefesin.Tests.Extensions.Wpf.Converters
{
    [TestClass]
    public class RectResizeConverterTests : BaseSimpleTest
    {
        #region RectResizeConverterProperties
        [TestClass]
        public class RectResizeConverterProperties : RectangleResizeConverterTests
        {

        }
        #endregion RectResizeConverterProperties

        #region RectResizeConverterConstruction
        [TestClass]
        public class RectResizeConverterConstruction : RectangleResizeConverterTests
        {

        }
        #endregion RectResizeConverterConstruction

        #region RectResizeConverterMethods
        [TestClass]
        public class RectResizeConverterMethods : RectangleResizeConverterTests
        {
            #region Convert
            [TestMethod]
            public void Convert()
            {
                RectangleResizeConverter rectangleResizeConverter = new RectangleResizeConverter();
                Rect sourceRect = new Rect(10, 10, 100, 100);
                double converterParameter = -10f;

                Rect targetRect = (Rect)rectangleResizeConverter.Convert(sourceRect, typeof(Rect), converterParameter, new System.Globalization.CultureInfo("De"));

                Assert.IsNotNull(targetRect);
                Assert.AreEqual(20, targetRect.X);
                Assert.AreEqual(20, targetRect.Y);
                Assert.AreEqual(80, targetRect.Width);
                Assert.AreEqual(80, targetRect.Height);
            }
            #endregion Convert

            #region ConvertWrongParams
            [TestMethod]
            public void ConvertWrongParams()
            {
                RectangleResizeConverter rectangleResizeConverter = new RectangleResizeConverter();
                Rect sourceRect = new Rect(10, 10, 100, 100);
                string converterParameter = "HeyDiHo";

                Rect targetRect = (Rect)rectangleResizeConverter.Convert(sourceRect, typeof(Rect), converterParameter, new System.Globalization.CultureInfo("De"));

                Assert.AreEqual(default(Rect), targetRect);
            }
            #endregion ConvertWrongParams

            #region ConvertBack
            [TestMethod]
            public void ConvertBack()
            {
                RectangleResizeConverter rectangleResizeConverter = new RectangleResizeConverter();
                Rect sourceRect = new Rect(10, 10, 100, 100);
                double converterParameter = -10f;

                Assert.ThrowsException<NotImplementedException>(() => { rectangleResizeConverter.ConvertBack(sourceRect, typeof(Rect), converterParameter, new System.Globalization.CultureInfo("De")); });
            }
            #endregion ConvertBack
        }
        #endregion RectResizeConverterMethods
    }
}
