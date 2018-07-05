using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    public static class MathHelper
    {
        #region GetAngleInDegrees: returns the current vehicle angle in degrees, or more important, in int
        /// <summary>
        /// returns the current vehicle angle in degrees, or more important, in int
        /// </summary>
        /// <returns>vehicle angle in degrees</returns>
        public static int GetAngleInDegrees(float angleInRadians)
        {
            int result = (int)((angleInRadians > 0 ? angleInRadians : (2 * Math.PI + angleInRadians)) * 360 / (2 * Math.PI)) % 360;
            if (result < 0)
            {
                result += 360;
            }
            return result;
        }
        #endregion GetAngleInDegrees

        #region GetAngleInRadians
        public static double GetAngleInRadians(float angle)
        {
            return (Math.PI / 180) * angle;
        }
        #endregion GetAngleInRadians
    }
}
