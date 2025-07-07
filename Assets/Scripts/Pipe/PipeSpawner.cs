using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    /// <summary>
    /// Spawns pipes at set intervals and random vertical positions during gameplay.
    /// </summary>
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private float spawnInterval = 4f;
    [SerializeField] private float MinY = -0.65f;
    [SerializeField] private float MaxY = 1.4f;

    Coroutine spawnCoroutine;

    /// <summary>
    /// Registers for restart events when enabled.
    /// </summary>
    private void OnEnable()
    {
        GameManager.Instance.onRestartGame.AddListener(OnRestartGameListener);
    }

    /// <summary>
    /// Unregisters from restart events when disabled.
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.onRestartGame.RemoveListener(OnRestartGameListener);
    }

    /// <summary>
    /// Starts spawning pipes when the game is in the gameplay phase.
    /// </summary>
    private void FixedUpdate()
    {
        if (spawnCoroutine == null && GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            spawnCoroutine = StartCoroutine(StartSpawn());
        }
    }

    /// <summary>
    /// Coroutine to wait for the spawn interval and then spawn a pipe.
    /// </summary>
    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnItem();
        spawnCoroutine = null;
    }

    /// <summary>
    /// Spawns a pipe at a random vertical position within the defined range.
    /// </summary>
    void SpawnItem()
    {
        if (GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            PipeController newPipe = PipePoolManager.Instance.GetItemFromPool();
            newPipe.gameObject.transform.parent = transform;
            newPipe.gameObject.transform.rotation = Quaternion.identity;
            Vector3 locPos = new Vector3(spawnPos.x, Random.Range(MinY, MaxY), spawnPos.z);
            newPipe.gameObject.transform.localPosition = locPos;
            newPipe.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Handles logic when the game is restarted (currently placeholder).
    /// </summary>
    private void OnRestartGameListener()
    {
        // later we can adjust spawn intervals for pipe if needed
    }
}
