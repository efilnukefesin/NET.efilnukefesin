using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using NET.efilnukefesin.Contracts.DependencyInjection;
using NET.efilnukefesin.Contracts.DependencyInjection.Classes;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using NET.efilnukefesin.Implementations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace NET.efilnukefesin.Implementations.DependencyInjection
{
    public class DiManager : BaseObject, IDependencyManager
    {
        #region Properties

        #region Singleton Properties

        private static DiManager instance;  // the instance of the singleton object, if it is already existing
        private static object lockSync = new object();  // lock synchronization object

        #endregion Singleton Properties

        #region container: the DI container to be abstracted
        /// <summary>
        /// the DI container to be abstracted
        /// </summary>
        private IContainer container;
        #endregion container

        #region builder: for AutoFac, we need a special builder for the container
        /// <summary>
        /// for AutoFac, we need a special builder for the container
        /// </summary>
        private ContainerBuilder builder;
        #endregion builder

        #region NumberOfRegistrations: returns the number of registrations of Interfaces and Classes
        /// <summary>
        /// returns the number of registrations of Interfaces and Classes
        /// </summary>
        public int NumberOfRegistrations
        {
            get
            {
                if (this.container == null)
                {
                    return 0;
                }
                else
                {
                    return this.container.ComponentRegistry.Registrations.Count(x => x.Services.Count() == 1) - 1;
                }
            }
        }
        #endregion NumberOfRegistrations

        private Dictionary<Type, Type> registeredTypes;

        private Dictionary<string, Type> typeTranslations;

        #endregion Properties

        #region Construction
        /// <summary>
        /// protected constructor
        /// </summary>
        protected DiManager()
        {
            this.initialize();
        }

        #endregion Construction

        #region Methods

        #region initialize
        private void initialize()
        {
            this.builder = new ContainerBuilder();
            this.builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            this.container = null;  //create the container, easy
            this.registeredTypes = new Dictionary<Type, Type>();
            this.typeTranslations = new Dictionary<string, Type>();
        }
        #endregion initialize

        #region Singleton Methods

        #region GetInstance: get the current instance
        /// <summary>
        /// get the current instance
        /// </summary>
        /// <returns>the. instance.</returns>
        public static DiManager GetInstance()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (DiManager.instance == null)  // if instance is not null, use existing one
            {
                lock (DiManager.lockSync)
                {
                    if (DiManager.instance == null)
                    {
                        DiManager.instance = new DiManager();
                    }
                }
            }
            return DiManager.instance;
        }
        #endregion GetInstance

        #endregion Singleton Methods

        #region Resolve: simple Target resolving
        /// <summary>
        /// simple Target resolving
        /// </summary>
        /// <typeparam name="T">the type to resolve</typeparam>
        /// <returns>a new object of the type</returns>
        public T Resolve<T>()
        {
            this.buildContainerIfNecessary();
            return (T)container.Resolve<T>();
        }
        #endregion Resolve

        #region Resolve
        public object Resolve(Type TypeToResolve)
        {
            this.buildContainerIfNecessary();
            return this.container.Resolve(TypeToResolve);
        }
        #endregion Resolve

        #region Resolve
        public T Resolve<T>(IDictionary<string, object> parameters)
        {
            this.buildContainerIfNecessary();

            List<Parameter> tempParameters = new List<Parameter>();
            foreach (var parameter in parameters)
            {
                tempParameters.Add(new NamedParameter(parameter.Key, parameter.Value));
            }
            return this.container.Resolve<T>(tempParameters);
        }
        #endregion Resolve

        #region Resolve
        public T Resolve<T>(IEnumerable<object> parameters)
        {
            this.buildContainerIfNecessary();

            return this.container.Resolve<T>(this.convertParameters(parameters.ToArray()));
        }
        #endregion Resolve

        #region Resolve
        public T Resolve<T>(params object[] parameters)
        {
            this.buildContainerIfNecessary();

            return this.container.Resolve<T>(this.convertParameters(parameters));
        }
        #endregion Resolve

        #region addToInternalRegister
        private void addToInternalRegister(Type interfaceType, Type serviceType)
        {
            if (!this.registeredTypes.ContainsKey(interfaceType))
            {
                this.registeredTypes.Add(interfaceType, serviceType);
            }
        }
        #endregion addToInternalRegister

        #region RegisterType: registers a type
        /// <summary>
        /// registers a type
        /// </summary>
        /// <typeparam name="TFrom">the base type / interface</typeparam>
        /// <typeparam name="TTo">the concrete implementation</typeparam>
        public void RegisterType<TFrom, TTo>() where TFrom : class where TTo : class, TFrom
        {
            this.builder.RegisterType<TTo>().As<TFrom>();
            this.addToInternalRegister(typeof(TFrom), typeof(TTo));
        }
        #endregion RegisterType

        #region RegisterTarget: registers a target for detailed parameter delivery
        /// <summary>
        /// registers a target for detailed parameter delivery
        /// </summary>
        /// <typeparam name="T">the target type</typeparam>
        /// <param name="parameters">the parameters in list form</param>
        public void RegisterTarget<T>(IEnumerable<ParameterInfoObject> parameters) where T : class
        {
            List<Parameter> paramsForBuilder = new List<Parameter>();
            foreach (var parameterInfoObject in parameters)
            {
                if (parameterInfoObject is TypeInstanceParameterInfoObject)
                {
                    TypeInstanceParameterInfoObject convertedParamaterInfoObject = parameterInfoObject as TypeInstanceParameterInfoObject;
                    Type parameterType = convertedParamaterInfoObject.Type;
                    if (this.typeTranslations.ContainsKey(parameterType.Name) || this.typeTranslations.ContainsKey(parameterType.FullName))
                    {
                        parameterType = this.typeTranslations.ContainsKey(parameterType.Name) ? this.typeTranslations[parameterType.Name] : this.typeTranslations[parameterType.FullName];
                    }
                    paramsForBuilder.Add(new TypedParameter(parameterType, convertedParamaterInfoObject.Instance));
                }
                else if (parameterInfoObject is DynamicParameterInfoObject)
                {
                    DynamicParameterInfoObject convertedParameterInfoObject = parameterInfoObject as DynamicParameterInfoObject;

                    Func<ParameterInfo, IComponentContext, bool> predicate = null;
                    Func<ParameterInfo, IComponentContext, object> valueAccessor = null;

                    if (convertedParameterInfoObject.Field == null)
                    {
                        //resolve only type
                        predicate = (pi, ctx) => pi.ParameterType.Equals(convertedParameterInfoObject.TypeToResolve)/* && pi.Name == "configSectionName"*/;
                        valueAccessor = (pi, ctx) => ctx.Resolve(convertedParameterInfoObject.TypeToResolve, this.convertParameters(convertedParameterInfoObject.Parameters));
                    }
                    //else
                    //{
                    //    //TODO: resolve type and call field value
                    //    //***
                    //    predicate = (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "configSectionName";
                    //    valueAccessor = (pi, ctx) => ctx.Resolve<>();
                    //}

                    paramsForBuilder.Add(new ResolvedParameter(predicate, valueAccessor));
                }
            }
            this.builder.RegisterType<T>().WithParameters(paramsForBuilder);
            this.addToInternalRegister(typeof(T), typeof(T));
        }
        #endregion RegisterTarget

        #region RegisterType: registers a type with a lifetime manager
        /// <summary>
        /// registers a type with a lifetime manager
        /// </summary>
        /// <typeparam name="TFrom">the base type / interface</typeparam>
        /// <typeparam name="TTo">the concrete implementation</typeparam>
        /// <param name="Lifetime">the desired lifetime</param>
        public void RegisterType<TFrom, TTo>(Lifetime Lifetime) where TFrom : class where TTo : class, TFrom
        {
            if (Lifetime == Lifetime.Singleton)
            {
                this.builder.RegisterType<TTo>().As<TFrom>().SingleInstance();
            }
            else
            {
                this.builder.RegisterType<TTo>().As<TFrom>();
            }
            this.addToInternalRegister(typeof(TFrom), typeof(TTo));
        }
        #endregion RegisterType

        #region registerType: internal wrapping method
        /// <summary>
        /// internal wrapping method
        /// </summary>
        /// <param name="TFrom">Interface type</param>
        /// <param name="TTo">Service Type</param>
        /// <param name="Lifetime">the Lifetime</param>
        private void registerType(Type TFrom, Type TTo, Lifetime Lifetime)
        {
            if (TFrom == null)
            {
                throw new ArgumentNullException("Type TFrom may not be null");
            }
            if (TTo == null)
            {
                throw new ArgumentNullException("Type TTo may not be null");
            }

            if (Lifetime == Lifetime.Singleton)
            {
                this.builder.RegisterType(TTo).As(TFrom).SingleInstance();
            }
            else
            {
                this.builder.RegisterType(TTo).As(TFrom);
            }
            this.addToInternalRegister(TFrom, TTo);
        }
        #endregion registerType

        #region RegisterInstance: Registers a concrete instance as a singleton
        /// <summary>
        /// Registers a concrete instance as a singleton
        /// </summary>
        /// <typeparam name="TFrom">the type of the instance for resolving</typeparam>
        /// <param name="Instance">the actual instance to register</param>
        public void RegisterInstance<TFrom>(TFrom Instance) where TFrom : class
        {
            this.builder.RegisterInstance(Instance).As<TFrom>();
            //this.container = this.builder.Build();
        }
        #endregion RegisterInstance

        #region Reset
        public void Reset()
        {
            this.initialize();
        }
        #endregion Reset

        #region SaveToXml
        public XElement SaveToXml(bool AddAssemblyDetail = false)
        {
            XElement result = new XElement(this.GetType().FullName);

            this.buildContainerIfNecessary();

            foreach (var element in this.container.ComponentRegistry.Registrations)
            {
                if (element.Services.Count() == 1)
                {
                    Service service = element.Services.FirstOrDefault();
                    Type serviceType = ((Autofac.Core.TypedService)service).ServiceType;

                    Type activatorLimitType = element.Activator.LimitType;

                    XElement xeRegistration = new XElement("Registration");
                    if (AddAssemblyDetail)
                    {
                        xeRegistration.Add(new XAttribute("Interface", serviceType.AssemblyQualifiedName));  //Interface
                        xeRegistration.Add(new XAttribute("Class", activatorLimitType.AssemblyQualifiedName));  //Class
                    }
                    else
                    {
                        xeRegistration.Add(new XAttribute("Interface", string.Format("{0}, {1}", serviceType.FullName, serviceType.AssemblyQualifiedName.Split(',')[1].Trim())));  //Interface
                        xeRegistration.Add(new XAttribute("Class", string.Format("{0}, {1}", activatorLimitType.FullName, activatorLimitType.AssemblyQualifiedName.Split(',')[1].Trim())));  //Class
                    }

                    if (element.Lifetime is Autofac.Core.Lifetime.RootScopeLifetime)
                    {
                        xeRegistration.Add(new XAttribute("Lifetime", "Singleton"));  //Lifetime
                    }

                    result.Add(xeRegistration);
                }
            }

            return result;
        }
        #endregion SaveToXml

        #region LoadFromXml
        public void LoadFromXml(XElement xeConfig)
        {
            DiManager.GetInstance().Dispose();
            foreach (XElement xeRegistration in xeConfig.Elements("Registration"))
            {
                string sInterface = xeRegistration.Attribute("Interface").Value;
                string sClass = xeRegistration.Attribute("Class").Value;
                string sLifetime = xeRegistration.Attribute("Lifetime")?.Value;

                Lifetime targetLifetime = Lifetime.NewInstanceEveryTime;
                if (sLifetime != null)
                {
                    if (sLifetime == "Singleton")
                    {
                        targetLifetime = Lifetime.Singleton;
                    }
                }

                Type targetInterface = Type.GetType(sInterface);
                if (targetInterface == null)
                {
                    throw new ArgumentException(string.Format("Could not get a Type from: {0}", sInterface));
                }
                Type targetClass = Type.GetType(sClass);
                if (targetClass == null)
                {
                    throw new ArgumentException(string.Format("Could not get a Type from: {0}", sClass));
                }

                DiManager.GetInstance().registerType(targetInterface, targetClass, targetLifetime);
            }
        }
        #endregion LoadFromXml

        #region buildContainerIfNecessary
        private void buildContainerIfNecessary()
        {
            if (this.container == null)
            {
                this.container = this.builder.Build();
            }
        }
        #endregion buildContainerIfNecessary

        #region IsRegistered
        public bool IsRegistered(Type TypeToCheck)
        {
            bool result = false;

            if (this.registeredTypes.ContainsKey(TypeToCheck))
            {
                result = true;
            }
            else if (this.registeredTypes.ContainsValue(TypeToCheck))
            {
                result = true;
            }

            return result;
        }
        #endregion IsRegistered

        #region IsRegistered
        public bool IsRegistered<T>()
        {
            bool result = false;

            if (this.registeredTypes.ContainsKey(typeof(T)))
            {
                result = true;
            }
            else if (this.registeredTypes.ContainsValue(typeof(T)))
            {
                result = true;
            }

            return result;
        }
        #endregion IsRegistered

        #region convertParameters: converts given parameter lists to AutoFac specific Parameter lists
        /// <summary>
        /// converts given parameter lists to AutoFac specific Parameter lists
        /// </summary>
        /// <param name="parameters">the params-Array</param>
        /// <returns>a list of AutoFac parameters</returns>
        private List<Parameter> convertParameters(object[] parameters)
        {
            List<Parameter> tempParameters = new List<Parameter>();
            foreach (var parameter in parameters)
            {
                Type parameterType = parameter.GetType();
                if (this.typeTranslations.ContainsKey(parameterType.Name) || this.typeTranslations.ContainsKey(parameterType.FullName))
                {
                    parameterType = this.typeTranslations.ContainsKey(parameterType.Name) ? this.typeTranslations[parameterType.Name] : this.typeTranslations[parameterType.FullName];
                }
                tempParameters.Add(new TypedParameter(parameterType, parameter));
            }

            return tempParameters;
        }
        #endregion convertParameters

        #region AddTypeTranslation: this method is used to add a translation for an (e.g.) mocked type, which could end with "Proxy" or something.
        /// <summary>
        /// this method is used to add a translation for an (e.g.) mocked type, which could end with "Proxy" or something.
        /// </summary>
        /// <param name="Value">the Source Type to be casted, as string as this could be dynamic</param>
        /// <param name="TargetType">the Type this Source Type shall be casted into</param>
        /// <returns>true, if successfuly added</returns>
        public bool AddTypeTranslation(string Value, Type TargetType)
        {
            bool result = false;

            if (!this.typeTranslations.ContainsKey(Value))
            {
                this.typeTranslations.Add(Value, TargetType);
                result = true;
            }

            return result;
        }
        #endregion AddTypeTranslation

        #region dispose
        protected override void dispose()
        {
            this.builder = null;
            this.container?.Dispose();
            this.container = null;
            this.registeredTypes.Clear();
            this.registeredTypes = null;
            this.typeTranslations.Clear();
            this.typeTranslations = null;
            DiManager.instance = null;
        }
        #endregion dispose

        #endregion Methods
    }
}
