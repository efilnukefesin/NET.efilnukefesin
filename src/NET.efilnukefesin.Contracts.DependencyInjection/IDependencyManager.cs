using NET.efilnukefesin.Contracts.DependencyInjection.Classes;
using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using NET.efilnukefesin.Contracts.Xml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NET.efilnukefesin.Contracts.DependencyInjection
{
    public interface IDependencyManager : ILoadFromXml, IDisposable
    {
        #region Methods

        T Resolve<T>();
        object Resolve(Type TypeToResolve);
        T Resolve<T>(IDictionary<string, object> parameters);
        T Resolve<T>(IEnumerable<object> parameters);
        T Resolve<T>(params object[] parameters);
        void RegisterType<TFrom, TTo>() where TFrom : class where TTo : class, TFrom;
        void RegisterType<TFrom, TTo>(Lifetime Lifetime) where TFrom : class where TTo : class, TFrom;
        void RegisterTarget<T>(IEnumerable<TypeInstanceParameterInfoObject> parameters) where T : class;
        void RegisterInstance<TFrom>(TFrom Instance) where TFrom : class;
        void Reset();
        XElement SaveToXml(bool AddAssemblyDetail = false);
        bool IsRegistered(Type TypeToCheck);
        bool IsRegistered<T>();

        #endregion Methods
    }
}
