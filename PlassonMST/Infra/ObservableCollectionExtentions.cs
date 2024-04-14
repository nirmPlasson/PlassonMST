namespace System.Collections.ObjectModel
{
    public static class ObservableCollectionExtentions
    {
        public static void Sort<T>(this ObservableCollection<T> collection, Comparer<T>? comparer = null)
        {
            var sortedList = new List<T>(collection);
            if (comparer == null)
            {
                sortedList.Sort();
            }
            else
            {
                sortedList.Sort(comparer);
            }
            for (int i = 0; i < sortedList.Count; i++)
            {
                int oldIndex = collection.IndexOf(sortedList[i]);
                int newIndex = i;
                if (oldIndex != newIndex)
                {
                    collection.Move(oldIndex, newIndex);
                }
            }
        }

        public static void AddSorted<T>(this ObservableCollection<T> collection, T newItem, Comparer<T>? comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }
            int newIndex = -1;
            foreach (T item in collection)
            {
                if (comparer.Compare(item, newItem) <= 0)
                {
                    newIndex = collection.IndexOf(item);
                    break;
                }
            }
            if (newIndex == -1)
            {
                collection.Add(newItem);
            }
            else
            {
                collection.Insert(newIndex, newItem);
            }
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> values, bool sorted = false)
        {
            if (values != null && values.Any())
            {
                foreach (T item in values)
                {
                    if (item != null)
                    {
                        if (false == collection.Contains(item))
                        {
                            if (sorted)
                            {
                                collection.Add(item);
                            }
                            else
                            {
                                collection.AddSorted(item);
                            }
                        }
                    }
                }
            }
        }
    }
}
