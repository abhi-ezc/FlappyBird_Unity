using UnityEngine;

[CreateAssetMenu(fileName = "PoolManagerSO", menuName = "Scriptable Objects/PoolManagerSO")]
public class PoolManagerSO : ScriptableObject
{
    public GameObject objectPrefab;
    public int poolSize = 32;
    public bool isExpandable = false;
}