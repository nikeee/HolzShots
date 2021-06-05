using System.Collections.Generic;

namespace HolzShots
{
    /// <summary>
    /// Ref: https://github.com/dotnet/runtime/issues/24227#issuecomment-737305756
    /// Modified: We use EqualityComparer<T>.Default.Equals for comparisons.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <remarks> IReadOnlyList doesn't have IndexOf() </remarks>
        public static int IndexOf<T>(this IReadOnlyList<T> readOnlyList, T element)
        {
            if (readOnlyList is IList<T> list)
                return list.IndexOf(element);

            for (int i = 0; i < readOnlyList.Count; i++)
                if (EqualityComparer<T>.Default.Equals(element, readOnlyList[i]))
                    return i;

            return -1;
        }
    }
}
