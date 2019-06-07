using System;
using System.Collections.Generic;
using System.Text;

namespace NET.efilnukefesin.Contracts.DependencyInjection.Classes
{
    public class DynamicParameterInfoObject : ParameterInfoObject
    {
        #region Properties

        #region ServiceInterface: the Service interface to use
        /// <summary>
        /// the Service interface to use
        /// </summary>
        public Type ServiceInterface { get; set; }
        #endregion ServiceInterface

        #region TypeToResolve: type to resolve dynamically
        /// <summary>
        /// type to resolve dynamically
        /// </summary>
        public Type TypeToResolve { get; set; }
        #endregion TypeToResolve

        #region Parameters: parameters to pass while resolving
        /// <summary>
        /// parameters to pass while resolving
        /// </summary>
        public object[] Parameters { get; set; }
        #endregion Parameters

        #region Field: the field to access, or Property
        /// <summary>
        /// the field to access, or Property
        /// </summary>
        public string Field { get; set; }
        #endregion Field

        #endregion Properties

        #region Construction

        //public DynamicParameterInfoObject(Type typeToResolve, string field, params object[] parameters)
        //{
        //    this.TypeToResolve = typeToResolve;
        //    this.Field = field;
        //    this.Parameters = parameters;
        //}

        public DynamicParameterInfoObject(Type typeToResolve, params object[] parameters)
        {
            this.TypeToResolve = typeToResolve;
            this.ServiceInterface = typeToResolve;
            this.Field = null;
            this.Parameters = parameters;
        }

        public DynamicParameterInfoObject(Type serviceInterface, Type typeToResolve, params object[] parameters)
        {
            this.TypeToResolve = typeToResolve;
            this.ServiceInterface = serviceInterface;
            this.Field = null;
            this.Parameters = parameters;
        }

        public DynamicParameterInfoObject(Type serviceInterface, Type typeToResolve)
        {
            this.TypeToResolve = typeToResolve;
            this.ServiceInterface = serviceInterface;
            this.Field = null;
            this.Parameters = null;
        }

        #endregion Construction

        #region Methods

        #region dispose
        protected override void dispose()
        {
            //TODO: implement
        }
        #endregion dispose

        #endregion Methods
    }
}
