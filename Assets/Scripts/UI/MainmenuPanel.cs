using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject TitleImage;
    [SerializeField] private GameObject TapImages;
    [SerializeField] private Button startButton;

    private void Start()
    {
        TitleImage.SetActive(true);
        TapImages.SetActive(true);
        startButton.gameObject.SetActive(true);
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
        GameManager.Instance.StartGetReady();
    }
}
