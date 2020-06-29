using System.Collections;
using System.Collections.Generic;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public interface IIndexedItem<out T>
    {
        int Index { get; }
        T Item { get; }
    }

    public sealed class IndexedItem<T> : IIndexedItem<T>
    {
        public IndexedItem(int index, T item)
        {
            Index = index;
            Item = item;
        }

        public int Index { get; }

        public T Item { get; }
    }

    public interface IIndexedEnumerable<out T> : IEnumerable<IIndexedItem<T>>
    {
    }

    public sealed class IndexedEnumerable<TItem> : IIndexedEnumerable<TItem>
    {
        private IReadOnlyList<IIndexedItem<TItem>> Values { get; }

        public IndexedEnumerable(IEnumerable<IndexedItem<TItem>> indexedItems)
        {
            Values = indexedItems.AsList();
        }

        public IEnumerator<IIndexedItem<TItem>> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}