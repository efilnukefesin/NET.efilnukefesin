using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    /// <summary>
    /// helper class for path concerns
    /// </summary>
    public static class PathHelper
    {
        #region GetProgramPath: return the current program path
        /// <summary>
        /// return the current program path
        /// </summary>
        /// <returns></returns>
        public static string GetProgramPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().CodeBase);
        }
        #endregion GetProgramPath

        #region GetProgramPath: return the current program path
        /// <summary>
        /// return the current program path
        /// </summary>
        /// <returns></returns>
        public static string GetProgramPath(Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.CodeBase);
        }
        #endregion GetProgramPath

        #region GetAbsoluteFileName: turns a relative file name into an absolute file name
        /// <summary>
        /// turns a relative file name into an absolute file name
        /// </summary>
        /// <param name="FilenameRelativeToProgramPath"></param>
        /// <returns></returns>
        public static string GetAbsoluteFileName(string FilenameRelativeToProgramPath)
        {
            string result = Path.Combine(PathHelper.GetProgramPath(), FilenameRelativeToProgramPath);
            result = result.Replace("#", "%23");
            return result;
        }
        #endregion GetAbsoluteFileName

        #region GetAbsoluteFileName
        public static string GetAbsoluteFileName(Assembly assembly, string FilenameRelativeToProgramPath)
        {
            string result = Path.Combine(PathHelper.GetProgramPath(assembly), FilenameRelativeToProgramPath);
            result = result.Replace("#", "%23");
            return result;
        }
        #endregion GetAbsoluteFileName
    }
}
