using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphoGeometry
{
    /// <summary>
    /// Extension class.
    /// </summary>
    internal static class Extension
    {
        /// <summary>
        /// Deconstruct an array with 1 item.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="items">Array to deconstruct.</param>
        /// <param name="t0">Item.</param>
        public static void Deconstruct<T>(this T[] items, out T t0)
        {
            t0 = items.Length > 0 ? items[0] : default(T);
        }

        /// <summary>
        /// Deconstruct an array with 2 item.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="items">Array to deconstruct.</param>
        /// <param name="t0">First item.</param>
        /// <param name="t1">Second item.</param>
        public static void Deconstruct<T>(this T[] items, out T t0, 
            out T t1)
        {
            t0 = items.Length > 0 ? items[0] : default(T);
            t1 = items.Length > 1 ? items[1] : default(T);
        }

        /// <summary>
        /// Deconstruct an array with 3 item.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="items">Array to deconstruct.</param>
        /// <param name="t0">First item.</param>
        /// <param name="t1">Second item.</param>
        /// <param name="t2">Third item.</param>
        public static void Deconstruct<T>(this T[] items, out T t0, 
            out T t1, out T t2)
        {
            t0 = items.Length > 0 ? items[0] : default(T);
            t1 = items.Length > 1 ? items[1] : default(T);
            t2 = items.Length > 2 ? items[2] : default(T);
        }
    }
}
