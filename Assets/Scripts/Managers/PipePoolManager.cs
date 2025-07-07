using System.Collections.Generic;
using UnityEngine;

public class PipePoolManager : MonoBehaviour
{
    public static PipePoolManager Instance { get; private set; }

    [SerializeField] private PoolManagerSO poolManagerConfig;

    private PoolManager<PipeController> poolManager = new PoolManager<PipeController>();

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

    private void OnEnable()
    {
        poolManager.Initialize(poolManagerConfig.poolSize, poolManagerConfig.objectPrefab, poolManagerConfig.isExpandable, transform);
    }

    private void Start()
    {
        poolManager.WarmPool();
    }

    public PipeController GetItemFromPool()
    {
       return poolManager.Get();
    }

    public void ReturnItemToPool(PipeController item)
    {
        poolManager.Return(item);
    }
}
