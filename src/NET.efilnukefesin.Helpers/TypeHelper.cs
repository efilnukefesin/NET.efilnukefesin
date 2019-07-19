using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NET.efilnukefesin.Helpers
{
    public static class TypeHelper
    {
        #region ImplementsInterface: shows, if a given type contains an interface
        /// <summary>
        /// shows, if a given type contains an interface
        /// </summary>
        /// <param name="type">the type to check</param>
        /// <param name="Interface">the interface it shall contain</param>
        /// <returns>true, if the Interface is implemented</returns>
        public static bool ImplementsInterface(Type type, Type Interface)
        {
            bool result = false;
            TypeInfo typeInfo = type.GetTypeInfo();

            if (typeInfo.ImplementedInterfaces.Any(x => x.Equals(Interface)))
            {
                result = true;
            }
            return result;
        }
        #endregion ImplementsInterface

        #region GetAlmostFullQualifiedTypeName
        public static string GetAlmostFullQualifiedTypeName(Type Type)
        {
            return string.Format("{0}, {1}", Type.FullName, Type.AssemblyQualifiedName.Split(',')[1].Trim());
        }
        #endregion GetAlmostFullQualifiedTypeName

        #region FindDeprecatedMethods: finds all methods marked with the ObsoleteAttribute of a Type
        /// <summary>
        /// finds all methods marked with the ObsoleteAttribute of a Type
        /// </summary>
        /// <param name="type">the type </param>
        /// <returns>a list of concerned method info objects</returns>
        public static IList<MethodInfo> FindDeprecatedMethods(Type type)
        {
            List<MethodInfo> result = new List<MethodInfo>();
            MethodInfo[] methodInfos = type.GetMethods();

            foreach (MethodInfo methodInfo in methodInfos)
            {
                object[] attributes = methodInfo.GetCustomAttributes(false);

                foreach (ObsoleteAttribute attribute in attributes.OfType<ObsoleteAttribute>())
                {
                    result.Add(methodInfo);
                }
            }

            return result;
        }
        #endregion FindDeprecatedMethods

        #region CreateInstance
        public static T CreateInstance<T>(params object[] Parameters)
        {
            T result = default;
            try
            {
                result = (T)TypeHelper.CreateInstance(typeof(T), Parameters);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion CreateInstance

        #region CreateInstance
        public static object CreateInstance(Type type, params object[] Parameters)
        {
            object result = default;
            try
            {
                result = Activator.CreateInstance(type, args: Parameters);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion CreateInstance

        #region CreateInstance
        public static object CreateInstance(string TypeName, params object[] Parameters)
        {
            Type type = Type.GetType(TypeName);
            object result = default;
            try
            {
                result = TypeHelper.CreateInstance(type, Parameters);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion CreateInstance

        #region HasAttribute
        public static bool HasAttribute<AttributeType>(Type typeToLookat) where AttributeType : Attribute //TODO: do equivalent for methods
        {
            bool result = false;
            var customAttribute = typeToLookat.GetCustomAttributes(typeof(AttributeType), true).FirstOrDefault() as AttributeType;
            if (customAttribute != null)
            {
                result = true;
            }
            return result;
        }
        #endregion HasAttribute
    }
}
