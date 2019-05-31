using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Implementations.Base
{
    [DataContract]
    public abstract class BaseObject : BaseDisposable, IBaseObject
    {
        #region Properties

        [DataMember]
        //public Guid Id { get; set; }
        public string Id { get; set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public DateTimeOffset CreationDate { get; private set; }

        [IgnoreDataMember]
        [XmlIgnore]
        public int CreationIndex { get; private set; }

        [IgnoreDataMember]
        [XmlIgnore]
        private static int highestCreationIndex = 0;

        #endregion Properties

        #region Construction

        public BaseObject()
        {
            this.CreationDate = DateTimeOffset.Now;
            //this.Id = Guid.NewGuid();
            this.Id = Guid.NewGuid().ToString();
            this.CreationIndex = BaseObject.highestCreationIndex;
            BaseObject.highestCreationIndex++;
        }

        public BaseObject(Guid Id)
        {
            this.CreationDate = DateTimeOffset.Now;
            //this.Id = Id;
            this.Id = Id.ToString();
        }

        #endregion Construction
    }
}
