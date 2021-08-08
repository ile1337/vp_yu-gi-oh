using System;
using System.ComponentModel;
using System.Linq;

namespace yu_gi_oh.Components.Actions
{
    public static class ActionExtensions
    {
        public readonly static Type[] actionTypes = new Type[] { typeof(MonsterActions), typeof(SpellActions), typeof(TrapActions) };

        public static string ToLabel<T>(this T val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }


        public static T ToAction<T>(this string label) where T : struct, Enum
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("ToAction<T>(): Must be of enum type", "T");

            foreach (T val in Enum.GetValues(type))
                if (val.ToLabel() == label)
                    return val;

            throw new ArgumentException("ToAction<T>(): Invalid description for enum " + type.Name, "label");

        }

        public static Enum FindAction(this string label)
        {
            return (
                from val in 
                    (from action in actionTypes 
                     select Enum.GetValues(action))
                     .SelectMany(item => item.Cast<Enum>())
                where val.ToLabel() == label 
                select val
                ).FirstOrDefault();

        }
    }
}
