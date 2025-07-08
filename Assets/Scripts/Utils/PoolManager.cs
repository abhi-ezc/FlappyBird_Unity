using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic object pool manager for Unity components.
/// </summary>
public class PoolManager<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();
    private List<T> itemsInUse = new List<T>();
    private int poolSize = 0;
    private GameObject objectPrefab;
    private bool isExpandable = false;
    private Transform parentTransform;

    /// <summary>
    /// Initializes the pool with the given configuration.
    /// </summary>
    public void Initialize(int poolSize, GameObject objectPrefab, bool isExpandable, Transform parentTransform)
    {

        if (!objectPrefab.TryGetComponent<T>(out T prefabComponent))
        {
            Debug.LogError($"Pool Manager failed to initialize, prefab does not have required component {nameof(T)}");
        }

        this.objectPrefab = objectPrefab;
        this.poolSize = poolSize;
        this.isExpandable = isExpandable;
        this.parentTransform = parentTransform;
    }

    /// <summary>
    /// Pre-instantiates objects to fill the pool.
    /// </summary>
    public void WarmPool()
    {
        while (pool.Count < poolSize)
        {
            T newObject = CreateNewObjectForPool();
            newObject.gameObject.SetActive(false);
            pool.Enqueue(newObject);
        }
    }

    /// <summary>
    /// Instantiates a new object for the pool.
    /// </summary>
    private T CreateNewObjectForPool()
    {
        if (!UnityEngine.Object.Instantiate(objectPrefab, parentTransform, false).TryGetComponent<T>(out T newObject))
        {
            throw new Exception($"prefab does not have required component {nameof(T)} ");
        }

        return newObject;
    }

    /// <summary>
    /// Retrieves an object from the pool, or creates a new one if expandable.
    /// </summary>
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
            Debug.LogWarning("Pool is empty and not expandable.");
            return null;
        }
    }

    /// <summary>
    /// Returns an object to the pool.
    /// </summary>
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

    /// <summary>
    /// Returns a list of objects to the pool.
    /// </summary>
    public void ReturnItems(List<T> items)
    {
        foreach (T item in items)
        {
            Return(item);
        }
    }

    /// <summary>
    /// Returns all objects in use to the pool.
    /// </summary>
    public void ReturnAll()
    {
        foreach (T item in itemsInUse)
        {
            Return(item);
        }
    }

    /// <summary>
    /// Retrieves a specified number of objects from the pool.
    /// </summary>
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