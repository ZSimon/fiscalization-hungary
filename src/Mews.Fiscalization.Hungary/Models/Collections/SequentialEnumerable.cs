using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mews.Fiscalization.Hungary.Utils;

namespace Mews.Fiscalization.Hungary.Models
{
    public interface ISequentialEnumerable<out T> : IReadOnlyList<IIndexedItem<T>>
    {
        IEnumerable<T> Items { get; }
    }

    public sealed class SequentialEnumerable<T> : ISequentialEnumerable<T>
    {
        private IReadOnlyList<IIndexedItem<T>> Values { get; }

        public IEnumerable<T> Items
        {
            get { return Values.Select(v => v.Item); }
        }

        public SequentialEnumerable(IEnumerable<IndexedItem<T>> indexedItems)
        {
            Values = indexedItems.AsList();
        }

        public IEnumerator<IIndexedItem<T>> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return Values.Count; }
        }

        public IIndexedItem<T> this[int index]
        {
            get { return Values.ElementAt(index); }
        }
    }

    public static class SequentialEnumerable
    {
        public static ISequentialEnumerable<T> FromPreordered<T>(IEnumerable<T> source, int startIndex)
        {
            return new SequentialEnumerable<T>(source.Select((item, index) => new IndexedItem<T>(startIndex + index, item)));
        }
    }
}