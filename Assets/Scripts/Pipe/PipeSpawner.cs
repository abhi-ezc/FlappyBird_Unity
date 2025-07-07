using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float MinY = -0.65f;
    [SerializeField] private float MaxY = 1.4f;

    Coroutine spawnCoroutine;

    private void OnEnable()
    {
        GameManager.Instance.onRestartGame.AddListener(OnRestartGameListener);
    }

    private void OnDisable()
    {
        GameManager.Instance.onRestartGame.RemoveListener(OnRestartGameListener);
    }

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
            PipeController newPipe = PipePoolManager.Instance.GetItemFromPool();
            newPipe.gameObject.transform.parent = transform;
            newPipe.gameObject.transform.rotation = Quaternion.identity;
            Vector3 locPos = new Vector3(spawnPos.x, Random.Range(MinY, MaxY), spawnPos.z);
            newPipe.gameObject.transform.localPosition = locPos;
            newPipe.gameObject.SetActive(true);
        }
    }

    private void OnRestartGameListener()
    {
        // later we can adjust spawn intervals for pipe if needed
    }
}
