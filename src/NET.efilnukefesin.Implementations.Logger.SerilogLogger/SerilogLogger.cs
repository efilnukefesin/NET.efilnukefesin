using NET.efilnukefesin.Contracts.Logger;
using NET.efilnukefesin.Contracts.Logger.Enums;
using NET.efilnukefesin.Implementations.Base;
using Serilog;
using System;

namespace NET.efilnukefesin.Implementations.Logger.SerilogLogger
{
    public class SerilogLogger : BaseObject, Contracts.Logger.ILogger
    {
        #region Properties

        public string Target { get; set; }
        public string LastEntry { get; set; }

        private Serilog.Core.Logger logger;

        #endregion Properties

        #region Construction

        public SerilogLogger()
        {
            this.logger = new LoggerConfiguration()
                .WriteTo.Async(a => a.Debug())
                //.WriteTo.Debug()  //TODO: change config for productive use
                .MinimumLevel.Debug()
                .CreateLogger();
            // https://github.com/serilog/serilog/wiki/Writing-Log-Events
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            this.Target = null;
        }
        #endregion dispose

        #region Log
        public void Log(string Text, LogLevel Severity = LogLevel.Info)
        {
            this.LastEntry = Text;
            switch (Severity)
            {
                case LogLevel.Debug:
                    this.logger.Debug(Text);
                    break;
                case LogLevel.Info:
                    this.logger.Information(Text);
                    break;
                case LogLevel.Warning:
                    this.logger.Warning(Text);
                    break;
                case LogLevel.Error:
                    this.logger.Error(Text);
                    break;
                case LogLevel.Fatal:
                    this.logger.Fatal(Text);
                    break;
                default:
                    this.logger.Information(Text);
                    break;
            }
        }
        #endregion Log

        #endregion Methods
    }
}
