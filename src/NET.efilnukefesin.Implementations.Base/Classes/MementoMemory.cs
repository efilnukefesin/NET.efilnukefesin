using NET.efilnukefesin.Contracts.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base.Classes
{
    internal class MementoMemory
    {
        #region Properties

        private string serializedString;
        private Type originalType;

        #endregion Properties

        #region Construction

        #endregion Construction

        #region Methods

        #region Memorize
        public void Memorize(IMementoOriginator Value)
        {
            this.originalType = Value.GetType();
            this.serializedString = JsonConvert.SerializeObject(Value);
        }
        #endregion Memorize

        #region Remember
        public object Remember()
        {
            object result = default;

            if (!string.IsNullOrEmpty(this.serializedString))
            {
                result = JsonConvert.DeserializeObject(this.serializedString, this.originalType);
            }

            return result;
        }
        #endregion Remember

        #endregion Methods

        #region Events

        #endregion Events
    }
}
