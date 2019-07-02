using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

namespace NET.efilnukefesin.Extensions
{
    public static class IEnumerableMementoExtensions
    {
        #region Properties

        private static Dictionary<string, Tuple<Type, string>> memory = new Dictionary<string, Tuple<Type, string>>();

        #endregion Properties

        #region Methods

        #region Save
        public static void Save<T>(this IEnumerable<T> enumerable, string name)
        {
            string nameOfList = name;
            string serializedList = JsonConvert.SerializeObject(enumerable);
            if (IEnumerableMementoExtensions.memory.ContainsKey(nameOfList))
            {
                //delete existing entry
                IEnumerableMementoExtensions.memory.Remove(nameOfList);
            }
            IEnumerableMementoExtensions.memory.Add(nameOfList, Tuple.Create<Type, string>(enumerable.GetType(), serializedList));
        }
        #endregion Save

        #region Restore
        public static void Restore<T>(this IList<T> list, string name)
        {
            string nameOfList = name;
            if (IEnumerableMementoExtensions.memory.ContainsKey(nameOfList))
            {
                var oldList = JsonConvert.DeserializeObject(IEnumerableMementoExtensions.memory[nameOfList].Item2, IEnumerableMementoExtensions.memory[nameOfList].Item1);
                //oldList = Convert.ChangeType(oldList, IEnumerableMementoExtensions.memory[nameOfList].Item1);
                //enumerable.ToList().RemoveAll(x => true);
                list.Clear();
                int cnt = 0;
                foreach (T item in (oldList as IEnumerable<T>))
                {
                    list.Add(item);
                    cnt++;
                }
            }
        }
        #endregion Restore

        #endregion Methods
    }
}
