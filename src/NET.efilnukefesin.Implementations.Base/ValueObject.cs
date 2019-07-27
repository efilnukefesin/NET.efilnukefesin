using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NET.efilnukefesin.Implementations.Base
{
    [DataContract]
    public class ValueObject<T> : BaseObject
    {
        #region Properties

        [DataMember]
        public T Value { get; set; }

        #endregion Properties

        #region Construction

        public ValueObject(T value) 
            : base()
        {
            this.Value = value;
        }

        [JsonConstructor]
        public ValueObject(Guid Id)
            : base(Id)
        {
            
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            Value = default;
        }
        #endregion dispose


        #endregion Methods
    }
}
