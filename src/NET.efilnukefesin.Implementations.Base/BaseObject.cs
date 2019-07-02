using NET.efilnukefesin.Contracts.Base;
using NET.efilnukefesin.Implementations.Base.Classes;
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
        public IBaseObject CreationPredecessor { get; set; } = default;

        [IgnoreDataMember]
        [XmlIgnore]
        public IBaseObject CreationSucessor { get; set; } = default;

        [IgnoreDataMember]
        [XmlIgnore]
        private static int highestCreationIndex = 0;

        [IgnoreDataMember]
        [XmlIgnore]
        private static IBaseObject lastCreatedBaseObject = default;

        [IgnoreDataMember]
        [XmlIgnore]
        private MementoMemory memory;

        #endregion Properties

        #region Construction

        public BaseObject()
        {
            this.CreationDate = DateTimeOffset.Now;
            this.memory = new MementoMemory();
            //this.Id = Guid.NewGuid();
            this.Id = Guid.NewGuid().ToString();
            this.CreationIndex = BaseObject.highestCreationIndex;
            BaseObject.highestCreationIndex++;
            this.CreationPredecessor = BaseObject.lastCreatedBaseObject;
            if (BaseObject.lastCreatedBaseObject != null)
            {
                BaseObject.lastCreatedBaseObject.CreationSucessor = this;
            }
            BaseObject.lastCreatedBaseObject = this;
        }

        public BaseObject(Guid Id)
        {
            this.CreationDate = DateTimeOffset.Now;
            //this.Id = Id;
            this.Id = Id.ToString();
        }

        #endregion Construction

        #region Methods

        #region Save
        public void Save()
        {
            this.memory.Memorize(this);
        }
        #endregion Save

        #region Restore
        public void Restore()
        {
            var oldThing = this.memory.Remember();
            if (oldThing != null)
            {
                foreach (var property in this.GetType().GetProperties())
                {
                    if (property.GetCustomAttributes(typeof(XmlIgnoreAttribute), false).GetLength(0) == 0)
                    {
                        property.SetValue(this, property.GetValue(oldThing, null), null);
                    }
                }
            }
        }
        #endregion Restore

        #region DiffersFromMemory
        public bool DiffersFromMemory()
        {
            return this.memory.IsDifferent(this);
        }
        #endregion DiffersFromMemory

        #endregion Methods
    }
}
