using System;
using System.Collections;
using System.Collections.Generic;

public class BTSortedList<T> : IList<T> where T : IComparable<T>
{
    private List<T> _items;

    public BTSortedList()
    {
        _items = new List<T>();
    }

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public T this[int index]
    {
        get => _items[index];
        set => throw new NotImplementedException();
    }

    public void Add(T item)
    {
        // Use a binary search to find the insertion point
        int left = 0;
        int right = _items.Count - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (item.CompareTo(_items[mid]) < 0)
                right = mid - 1;
            else
                left = mid + 1;
        }

        _items.Insert(left, item);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool Contains(T item)
    {
        return _items.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _items.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return _items.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        return _items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    public T RemoveFirst()
    {
        T temp = _items[0];
        RemoveAt(0);
        return temp;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _items.GetEnumerator();
    }
}
