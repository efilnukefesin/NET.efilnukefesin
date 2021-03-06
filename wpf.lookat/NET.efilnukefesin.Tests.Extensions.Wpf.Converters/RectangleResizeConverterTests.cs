﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NET.efilnukefesin.BaseClasses.Test;
using NET.efilnukefesin.Extensions.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

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
            [DataTestMethod]
            [DataRow("-10")]
            [DataRow("-10/-10")]
            public void Convert(string Parameter)
            {
                RectangleResizeConverter rectangleResizeConverter = new RectangleResizeConverter();
                Rectangle sourceRect = new Rectangle();
                sourceRect.Width = 100;
                sourceRect.Height = 100;

                Rect targetRect = (Rect)rectangleResizeConverter.Convert(sourceRect, typeof(Rect), Parameter, new System.Globalization.CultureInfo("De"));

                Assert.IsNotNull(targetRect);
                Assert.AreEqual(10, targetRect.X);
                Assert.AreEqual(10, targetRect.Y);
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
        #endregion RectangleResizeConverterMethods
    }
}
