using UnityEngine;
using UnityEngine.Events;

public enum EGamePhase
{
    MainMenu,
    GamePlay,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public EGamePhase gamePhase { get; private set; }
    public UnityEvent onGameOver;
    private int gameScore = 0;

    private void Awake()
    {
        if(Instance == null)
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
    }

    void Start()
    {
        SetGamePhase(EGamePhase.GamePlay);
    }

    void Update()
    {
        
    }

    void SetGamePhase(EGamePhase gamePhase)
    {
        this.gamePhase = gamePhase;
    }

    public void SetGameOver()
    {
       SetGamePhase(EGamePhase.GameOver);
        onGameOver?.Invoke();
    }

    public void OnTriggerScore()
    {
        AudioManager.Instance.playSoundEffect(EAudioClipType.Score);
        gameScore++;
    }
}
