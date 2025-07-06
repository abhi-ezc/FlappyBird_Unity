using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject TitleImage;
    [SerializeField] private GameObject TapImages;
    [SerializeField] private GameObject GetReadyImage;
    [SerializeField] private Image GetReadyCounterParent;
    [SerializeField] private Button startButton;

    [Header("Counter Images")]
    [SerializeField] private Sprite[] getReadySprites;

    private int currentCounterIndex = -1;
    private Coroutine counterCoroutine;
    private bool canRunCounter = false;

    private void Start()
    {
        TitleImage.SetActive(true);
        TapImages.SetActive(true);
        startButton.gameObject.SetActive(true);
        GetReadyImage.SetActive(false);
        GetReadyCounterParent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        TitleImage.SetActive(false);
        TapImages.SetActive(false);
        GetReadyImage.SetActive(true);
        canRunCounter = true;
    }

    private void LateUpdate()
    {
        if(counterCoroutine == null && canRunCounter)
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
        GetReadyImage.SetActive(false);
        GetReadyCounterParent.sprite = getReadySprites[currentCounterIndex];
        GetReadyCounterParent.gameObject.SetActive(true);
    }
}
