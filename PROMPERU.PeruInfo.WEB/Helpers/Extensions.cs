using System.Collections.Generic;
using System.Linq;

namespace PROMPERU.PeruInfo.WEB.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] source, int size)
        {
            return source.Select((s, i) => source.Skip(i * size).Take(size))
                .Where(a => a.Any());
        }

        public static List<List<T>> Split<T>(this List<T> source, int size)
        {
            return source.Select((s, i) => source.Skip(i * size).Take(size).ToList())
                .Where(a => a.Any())
                .ToList();
        }
    }
}