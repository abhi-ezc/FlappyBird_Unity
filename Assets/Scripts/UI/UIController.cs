using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGamePhaseChanged.AddListener(OnGamePhaseChanged);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePhaseChanged.RemoveListener(OnGamePhaseChanged);
    }


    void OnGamePhaseChanged(EGamePhase gamePhase)
    {
        mainMenuPanel.SetActive(gamePhase == EGamePhase.MainMenu);
        gamePlayPanel.SetActive(gamePhase == EGamePhase.GamePlay || gamePhase == EGamePhase.GameOver); // for gameover also we are showing gameplaypanel
        gameOverPanel.SetActive(gamePhase == EGamePhase.GameOver);
    }
}
