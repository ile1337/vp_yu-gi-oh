using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace yu_gi_oh.Components.Actions
{
    class ActionUtilities
    {
        public static string[] FilterLabelActions<T, K>() where T : struct, Enum, IConvertible => LabelActions(FilterActions<T, K>());

        private static string[] LabelActions<T>(IEnumerable<T> actions) => actions.Select(item => item.ToLabel()).ToArray();

        public static IEnumerable<T> FilterActions<T, K>() where T : struct, Enum, IConvertible => (from val in typeof(T).GetFields()
                                                                                             where val.GetCustomAttribute(typeof(K)) != null
                                                                                             select (T)val.GetRawConstantValue());

    }
}
