using UnityEngine;

/// <summary>
/// ScriptableObject for configuring object pools.
/// </summary>
[CreateAssetMenu(fileName = "PoolManagerSO", menuName = "Scriptable Objects/PoolManagerSO")]
public class PoolManagerSO : ScriptableObject
{
    /// <summary>
    /// Prefab to be pooled.
    /// </summary>
    public GameObject objectPrefab;
    /// <summary>
    /// Initial pool size.
    /// </summary>
    public int poolSize = 32;
    /// <summary>
    /// Whether the pool can expand beyond its initial size.
    /// </summary>
    public bool isExpandable = false;
}