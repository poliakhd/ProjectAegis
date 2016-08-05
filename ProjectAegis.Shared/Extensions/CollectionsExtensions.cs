using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace ProjectAegis.Shared.Extensions
{
    public static class CollectionsExtensions
    {
        public static BindableCollection<T> ToBindableCollection<T>(this IEnumerable<T> source)
        {
            return new BindableCollection<T>(source);
        }
    }
}