using System.Collections.Generic;
using UnityEngine;

public class PipePoolManager : MonoBehaviour
{
    public static PipePoolManager Instance { get; private set; }

    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private int poolSize = 10;

    Queue<GameObject> pool;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pool = new Queue<GameObject>();
        WarmPool();
    }

    private void WarmPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(pipePrefab, transform);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetItemFromPool()
    {
        if(pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
           return Instantiate(pipePrefab, transform);
        }
    }

    public void ReturnItemToPool(GameObject item)
    {
        item.SetActive(false);
        item.transform.parent = transform;
        pool.Enqueue(item);
    }
}
