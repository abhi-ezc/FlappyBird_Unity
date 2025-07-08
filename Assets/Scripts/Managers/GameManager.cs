using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enum representing the different phases of the game.
/// </summary>
public enum EGamePhase
{
    MainMenu,
    GetReady,
    GamePlay,
    GameOver
}

/// <summary>
/// Manages the overall game state, score, and game phase transitions.
/// Implements the Singleton pattern.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public EGamePhase gamePhase { get; private set; }
    public UnityEvent<EGamePhase> onGamePhaseChanged;
    public UnityEvent<int> onScoreChanged;
    public UnityEvent onGameOver;
    public UnityEvent onRestartGame;
    private int gameScore = 0;
    private int highScore = 0;
    private const int TargetFrameRate = 60;

    /// <summary>
    /// Ensures only one instance of GameManager exists.
    /// </summary>
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

    /// <summary>
    /// Initializes UnityEvents when enabled.
    /// </summary>
    private void OnEnable()
    {
        onGameOver = new UnityEvent();
        onGamePhaseChanged = new UnityEvent<EGamePhase>();
        onScoreChanged = new UnityEvent<int>();
        onRestartGame = new UnityEvent();
    }

    void Start()
    {
        Application.targetFrameRate = TargetFrameRate;
        SetGamePhase(EGamePhase.MainMenu);
        LoadPrefs();
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
        UpdatePrefs();
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
        UpdateHighScore();
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

    private void LoadPrefs()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void UpdatePrefs()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public int GetHighScore()
    {
        return highScore;
    }

    private void UpdateHighScore()
    {
        if (gameScore > highScore)
        {
            highScore = gameScore;
        }
    }
}
