using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Contracts.Logger.Enums;
using System;

namespace NET.efilnukefesin.Contracts.Logger
{
    public interface ILogger : IBaseObject
    {
        #region Properties

        string Target { get; set; }

        #endregion Properties

        #region Methods

        void Log(string Text, LogLevel Severity = LogLevel.Info);

        #endregion Methods
    }
}
