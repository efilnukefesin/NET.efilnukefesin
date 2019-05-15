using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using NET.efilnukefesin.Contracts.DependencyInjection;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NET.efilnukefesin.Implementations.DependencyInjection
{
    public class DiManager : IDependencyManager
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

        #endregion Properties

        #region Construction
        /// <summary>
        /// protected constructor
        /// </summary>
        protected DiManager()
        {
            this.builder = new ContainerBuilder();
            this.builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            this.container = null;  //create the container, easy
            this.registeredTypes = new Dictionary<Type, Type>();
        }

        #endregion Construction

        #region Methods

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

            List<Parameter> tempParameters = new List<Parameter>();
            foreach (var parameter in parameters)
            {
                tempParameters.Add(new TypedParameter(parameter.GetType(), parameter));
            }
            return this.container.Resolve<T>(tempParameters);
        }
        #endregion Resolve

        #region Resolve
        public T Resolve<T>(params object[] parameters)
        {
            this.buildContainerIfNecessary();

            List<Parameter> tempParameters = new List<Parameter>();
            for (int i = 0; i < parameters.Length; i++)
            {
                object parameter = parameters[i];
                tempParameters.Add(new TypedParameter(parameter.GetType(), parameter));
            }
            return this.container.Resolve<T>(tempParameters);
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

        public void RegisterType<TFrom, TTo>(params object[] parameters) where TFrom : class where TTo : class, TFrom
        {
            this.builder.RegisterType<TTo>().As<TFrom>().WithParameters();
            this.addToInternalRegister(typeof(TFrom), typeof(TTo));
        }

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

        public void RegisterType<TFrom, TTo>(Lifetime Lifetime, params object[] parameters) where TFrom : class where TTo : class, TFrom
        {
            if (Lifetime == Lifetime.Singleton)
            {
                this.builder.RegisterType<TTo>().As<TFrom>().SingleInstance().WithParameters();
            }
            else
            {
                this.builder.RegisterType<TTo>().As<TFrom>().WithParameters();
            }
            this.addToInternalRegister(typeof(TFrom), typeof(TTo));
        }

        #region registerType
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

        #region Dispose: disposes the container
        /// <summary>
        /// disposes the container
        /// </summary>
        public void Dispose()
        {
            this.builder = null;
            this.container?.Dispose();
            this.container = null;
            DiManager.instance = null;
        }
        #endregion Dispose

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

        #endregion Methods
    }
}
