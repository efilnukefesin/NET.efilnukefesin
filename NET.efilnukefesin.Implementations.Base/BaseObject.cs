using NET.efilnukefesin.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NET.efilnukefesin.Implementations.Base
{
    public abstract class BaseObject : BaseDisposable, IBaseObject
    {
        #region Properties

        public Guid Id { get; set; }

        [XmlIgnore]
        public DateTimeOffset CreationDate { get; private set; }

        #endregion Properties

        #region Construction

        public BaseObject()
        {
            this.CreationDate = DateTimeOffset.Now;
            this.Id = Guid.NewGuid();
        }

        public BaseObject(Guid Id)
        {
            this.CreationDate = DateTimeOffset.Now;
            this.Id = Id;
        }

        #endregion Construction
    }
}
