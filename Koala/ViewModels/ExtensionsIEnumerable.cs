using System.Collections.Generic;

namespace Koala.ViewModels
{
    public static class ExtensionsIEnumerable
    {
        public static MutableObservableCollection<T> Convert<T>(this IEnumerable<T> original)
        {
            return new MutableObservableCollection<T>(original);
        }
    }
}
