using UnityEngine;

/// <summary>
/// Controls the visibility of different UI panels based on the current game phase.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject getReadyPanel;



    /// <summary>
    /// Registers for game phase change events when enabled.
    /// </summary>
    private void OnEnable()
    {
        GameManager.Instance.onGamePhaseChanged.AddListener(OnGamePhaseChanged);
    }

    /// <summary>
    /// Unregisters from game phase change events when disabled.
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.onGamePhaseChanged.RemoveListener(OnGamePhaseChanged);
    }

    /// <summary>
    /// Updates the visibility of UI panels based on the current game phase.
    /// </summary>
    void OnGamePhaseChanged(EGamePhase gamePhase)
    {
        mainMenuPanel.SetActive(gamePhase == EGamePhase.MainMenu);
        getReadyPanel.SetActive(gamePhase == EGamePhase.GetReady);
        gamePlayPanel.SetActive(gamePhase == EGamePhase.GamePlay || gamePhase == EGamePhase.GameOver); // temporary
        gameOverPanel.SetActive(gamePhase == EGamePhase.GameOver);
    }
}
