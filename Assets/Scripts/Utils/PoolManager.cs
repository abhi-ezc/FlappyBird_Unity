using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();
    private List<T> itemsInUse = new List<T>();
    private int poolSize = 0;
    private GameObject objectPrefab;
    private bool isExpandable = false;
    private Transform parentTransform;

    public void Initialize(int poolSize, GameObject objectPrefab, bool isExpandable, Transform parentTransform)
    {

        if (!objectPrefab.TryGetComponent<T>(out T prefabComponent))
        {
            Debug.LogError($"Pool Manager failed to initialize, prefab does not have required component {nameof(T)}");
            throw new Exception($"Pool Manager failed to initialize, prefab does not have required component {nameof(T)}");
        }

        this.objectPrefab = objectPrefab;
        this.poolSize = poolSize;
        this.isExpandable = isExpandable;
        this.parentTransform = parentTransform;
    }

    public void WarmPool()
    {
        while (pool.Count < poolSize)
        {
            T newObject = CreateNewObjectForPool();
            newObject.gameObject.SetActive(false);
            pool.Enqueue(newObject);
        }
    }

    private T CreateNewObjectForPool()
    {
        if (!UnityEngine.Object.Instantiate(objectPrefab, parentTransform, false).TryGetComponent<T>(out T newObject))
        {
            throw new Exception($"prefab does not have required component {nameof(T)} ");
        }

        return newObject;
    }

    public T Get()
    {
        if (pool.Count > 0)
        {
            T item = pool.Dequeue();
            itemsInUse.Add(item);
            return item;
        }
        else if (isExpandable)
        {
            T item = CreateNewObjectForPool();
            itemsInUse.Add(item);
            return item;
        }
        else
        {
            return null;
        }
    }

    public void Return(T item)
    {
        if (!itemsInUse.Contains(item))
        {
            return;
        }

        if (pool.Count == poolSize)
        {
            UnityEngine.Object.Destroy(item);
        }
        else
        {
            item.gameObject.SetActive(false);
            item.gameObject.transform.SetParent(parentTransform, false);
            itemsInUse.Remove(item);
            pool.Enqueue(item);
        }
    }

    public void ReturnItems(List<T> items)
    {
        foreach (T item in items)
        {
            Return(item);
        }
    }

    public void ReturnAll()
    {
        foreach (T item in itemsInUse)
        {
            Return(item);
        }
    }

    public List<T> GetN(int n, bool canSetActive = false)
    {
        List<T> listN = new List<T>();
        while (n > 0)
        {
            T newItem = Get();
            newItem.gameObject.SetActive(canSetActive);
            listN.Add(newItem);
            n--;
        }

        return listN;
    }
}