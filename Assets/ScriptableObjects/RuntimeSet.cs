using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> Items = new List<T>();

    public void Add(T item)
    {
        if (!Items.Contains(item))
            Items.Add(item);
    }

    public void Remove(T item)
    {
        if (Items.Contains(item))
            Items.Remove(item);
    }

    public void Swap(int indexA, int indexB)
    {
        if (Items.Count <= indexA || Items.Count <= indexB)
        {
            Debug.LogError("Nyt meni pieleen, indeksi yli listan pituuden");
            return;
        }
        (Items[indexA], Items[indexB]) = (Items[indexB], Items[indexA]); // Tuplella taitaa toimia ?!
    }
}
