using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 4f;

    Coroutine spawnCoroutine;

    private void FixedUpdate()
    {
        if (spawnCoroutine == null && GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            spawnCoroutine = StartCoroutine(StartSpawn());
        }
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnItem();
        spawnCoroutine = null;
    }

    void SpawnItem()
    {
        if(GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            GameObject go = PipePoolManager.Instance.GetItemFromPool();
            go.transform.parent = transform;
            go.transform.rotation = Quaternion.identity;
            go.transform.localPosition = spawnPos;
            go.SetActive(true);
        }
    }
}
