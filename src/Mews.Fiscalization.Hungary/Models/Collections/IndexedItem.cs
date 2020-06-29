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
}