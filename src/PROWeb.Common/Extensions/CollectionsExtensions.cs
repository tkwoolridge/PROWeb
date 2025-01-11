namespace PROWeb.Common.Extensions
{
    public static class CollectionsExtensions
    {
        public static TItem? Find<TItem>(this IEnumerable<TItem> items, Func<TItem, IEnumerable<TItem>?> getChildren, Func<TItem, bool> predict)
        {
            foreach (var item in items)
            {
                if (item.Find(i => getChildren(i), predict) is { } result)
                {
                    return result;
                }
            }

            return default;
        }

        public static TItem? Find<TItem>(this TItem root, Func<TItem, IEnumerable<TItem>?> getChildren, Func<TItem, bool> predict)
        {
            Queue<TItem> queue = new Queue<TItem>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                if (queue.Dequeue() is { } item && predict(item))
                {
                    return item;
                }

                if (getChildren(root) is { } children)
                {
                    foreach (TItem child in children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return default;
        }
    }
}
