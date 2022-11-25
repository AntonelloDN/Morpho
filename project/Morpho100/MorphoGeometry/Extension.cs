using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphoGeometry
{
    internal static class Extension
    {
        public static void Deconstruct<T>(this T[] items, out T t0)
        {
            t0 = items.Length > 0 ? items[0] : default(T);
        }

        public static void Deconstruct<T>(this T[] items, out T t0, out T t1)
        {
            t0 = items.Length > 0 ? items[0] : default(T);
            t1 = items.Length > 1 ? items[1] : default(T);
        }

        public static void Deconstruct<T>(this T[] items, out T t0, out T t1, out T t2)
        {
            t0 = items.Length > 0 ? items[0] : default(T);
            t1 = items.Length > 1 ? items[1] : default(T);
            t2 = items.Length > 2 ? items[2] : default(T);
        }
    }
}
