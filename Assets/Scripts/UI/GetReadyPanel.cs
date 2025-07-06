using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        GetReadyTextImageBlock.SetActive(true);
        CounterParent.gameObject.SetActive(false);
        canRunCounter = true;
    }

    private void OnDisable()
    {
        counterCoroutine = null;
        canRunCounter = false;
        currentCounterIndex = -1;
    }

    private void LateUpdate()
    {
        if (counterCoroutine == null && canRunCounter)
        {
            counterCoroutine = StartCoroutine(UpdateCounterImage());
        }
    }

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
