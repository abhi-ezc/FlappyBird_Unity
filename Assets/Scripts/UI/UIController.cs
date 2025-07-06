using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject getReadyPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.onGamePhaseChanged.AddListener(OnGamePhaseChanged);
    }

    private void OnDisable()
    {
        GameManager.Instance.onGamePhaseChanged.RemoveListener(OnGamePhaseChanged);
    }


    void OnGamePhaseChanged(EGamePhase gamePhase)
    {
        mainMenuPanel.SetActive(gamePhase == EGamePhase.MainMenu);
        getReadyPanel.SetActive(gamePhase == EGamePhase.GetReady);
        gamePlayPanel.SetActive(gamePhase == EGamePhase.GamePlay || gamePhase == EGamePhase.GameOver); // temporary
        gameOverPanel.SetActive(gamePhase == EGamePhase.GameOver);
    }
}
