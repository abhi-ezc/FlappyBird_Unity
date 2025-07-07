using UnityEngine;
using UnityEngine.Events;

public enum EGamePhase
{
    MainMenu,
    GetReady,
    GamePlay,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public EGamePhase gamePhase { get; private set; }
    public UnityEvent<EGamePhase> onGamePhaseChanged;
    public UnityEvent<int> onScoreChanged;
    public UnityEvent onGameOver;
    public UnityEvent onRestartGame;
    private int gameScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        onGameOver = new UnityEvent();
        onGamePhaseChanged = new UnityEvent<EGamePhase>();
        onScoreChanged = new UnityEvent<int>();
        onRestartGame = new UnityEvent();
    }

    void Start()
    {
        SetGamePhase(EGamePhase.MainMenu);
    }

    void Update()
    {

    }

    void SetGamePhase(EGamePhase gamePhase)
    {
        this.gamePhase = gamePhase;
        onGamePhaseChanged?.Invoke(gamePhase);
    }

    public void SetGameOver()
    {
        SetGamePhase(EGamePhase.GameOver);
        onGameOver?.Invoke();
    }

    public void OnTriggerScore()
    {
        AudioManager.Instance.playSoundEffect(EAudioClipType.Score);
        IncrementScore();
    }

    public void StartGetReady()
    {
        SetGamePhase(EGamePhase.GetReady);
    }

    public void StartGame()
    {
        ResetScore();
        SetGamePhase(EGamePhase.GamePlay);  
    }

    private void ResetScore()
    {
        gameScore = 0;
        onScoreChanged?.Invoke(gameScore);
    }

    private void IncrementScore()
    {
        gameScore++;
        onScoreChanged?.Invoke(gameScore);
    }

    public void RestartGame()
    {
        onRestartGame?.Invoke();
        StartGetReady();
    }

    public int GetCurrentScore()
    {
        return gameScore;
    }
}
