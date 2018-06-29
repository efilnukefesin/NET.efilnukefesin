using NET.efilnukefesin.Contracts.DependencyInjection.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NET.efilnukefesin.Contracts.DependencyInjection
{
    public interface IDependencyManager : IDisposable
    {
        #region Methods

        T Resolve<T>();
        object Resolve(Type TypeToResolve);
        T Resolve<T>(IDictionary<string, object> parameters);
        void RegisterType<TFrom, TTo>() where TFrom : class where TTo : class, TFrom;
        void RegisterType<TFrom, TTo>(Lifetime Lifetime) where TFrom : class where TTo : class, TFrom;
        void RegisterInstance<TFrom>(TFrom Instance) where TFrom : class;
        XElement SaveToXml(bool AddAssemblyDetail = false);
        void LoadFromXml(XElement xeConfig);

        #endregion Methods
    }
}
