using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a pool of PipeController objects for efficient reuse.
/// Implements the Singleton pattern.
/// </summary>
public class PipePoolManager : MonoBehaviour
{
    public static PipePoolManager Instance { get; private set; }

    [SerializeField] private PoolManagerSO poolManagerConfig;

    private PoolManager<PipeController> poolManager = new PoolManager<PipeController>();

    /// <summary>
    /// Ensures only one instance of PipePoolManager exists.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the pool manager with configuration on enable.
    /// </summary>
    private void OnEnable()
    {
        poolManager.Initialize(poolManagerConfig.poolSize, poolManagerConfig.objectPrefab, poolManagerConfig.isExpandable, transform);
    }

    /// <summary>
    /// Warms up the pool at the start of the game.
    /// </summary>
    private void Start()
    {
        poolManager.WarmPool();
    }

    /// <summary>
    /// Retrieves a PipeController from the pool.
    /// </summary>
    public PipeController GetItemFromPool()
    {
        return poolManager.Get();
    }

    /// <summary>
    /// Returns a PipeController to the pool.
    /// </summary>
    public void ReturnItemToPool(PipeController item)
    {
        poolManager.Return(item);
    }
}
