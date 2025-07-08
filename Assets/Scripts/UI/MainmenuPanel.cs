using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the main menu UI panel, including the start button and title/tap images.
/// </summary>
public class MainmenuPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject TitleImage;
    [SerializeField] private GameObject TapImages;
    [SerializeField] private Button startButton;

    /// <summary>
    /// Registers the start button click event when enabled.
    /// </summary>
    private void OnEnable()
    {
        TitleImage.SetActive(true);
        TapImages.SetActive(true);
        startButton.gameObject.SetActive(true);
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    /// <summary>
    /// Unregisters the start button click event when disabled.
    /// </summary>
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    /// <summary>
    /// Handles the start button click event to begin the game.
    /// </summary>
    private void OnStartButtonClicked()
    {
        GameManager.Instance.StartGetReady();
    }
}
