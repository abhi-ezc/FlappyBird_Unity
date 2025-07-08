using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the Get Ready UI panel, including countdown and transition to gameplay.
/// </summary>
public class GetReadyPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject GetReadyTextImageBlock;
    [SerializeField] private Image CounterParent;

    [Header("Counter Images")]
    [SerializeField] private Sprite[] getReadySprites;

    private int currentCounterIndex = -1;
    private Coroutine counterCoroutine;
    private bool canRunCounter = false;

    /// <summary>
    /// Prepares the panel and enables the countdown when enabled.
    /// </summary>
    private void OnEnable()
    {
        GetReadyTextImageBlock.SetActive(true);
        CounterParent.gameObject.SetActive(false);
        canRunCounter = true;
    }

    /// <summary>
    /// Resets the countdown state when disabled.
    /// </summary>
    private void OnDisable()
    {
        counterCoroutine = null;
        canRunCounter = false;
        currentCounterIndex = -1;
    }

    /// <summary>
    /// Starts the countdown coroutine if not already running.
    /// </summary>
    private void LateUpdate()
    {
        if (counterCoroutine == null && canRunCounter)
        {
            counterCoroutine = StartCoroutine(UpdateCounterImage());
        }
    }

    /// <summary>
    /// Coroutine to update the countdown image and start the game when finished.
    /// </summary>
    private IEnumerator UpdateCounterImage()
    {
        yield return new WaitForSeconds(1f);
        currentCounterIndex++;
        if (currentCounterIndex >= getReadySprites.Length)
        {
            canRunCounter = false;
            GameManager.Instance.StartGame();
            yield break;
        }
        counterCoroutine = null;
        GetReadyTextImageBlock.SetActive(false);
        CounterParent.sprite = getReadySprites[currentCounterIndex];
        CounterParent.gameObject.SetActive(true);
    }
}
