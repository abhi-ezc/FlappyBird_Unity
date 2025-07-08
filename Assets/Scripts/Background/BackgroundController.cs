using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controls the scrolling background and floor, syncing with game phase and restart events.
/// </summary>
public class BackgroundController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer floorSR;
    [SerializeField] private SpriteRenderer skySR;

    private float scrollTime;
    private const string scrollTimeKey = "_ScrollTime";
    private bool canAddScrollTime = true;

    /// <summary>
    /// Registers for game phase and restart events when enabled.
    /// </summary>
    private void OnEnable()
    {
        scrollTime = 0;
        GameManager.Instance.onGamePhaseChanged.AddListener(OnGamePhaseChangedListener);
        GameManager.Instance.onRestartGame.AddListener(OnRestartGameListener);
    }

    /// <summary>
    /// Unregisters from game phase and restart events when disabled.
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.onGamePhaseChanged.RemoveListener(OnGamePhaseChangedListener);
        GameManager.Instance.onRestartGame.RemoveListener(OnRestartGameListener);
    }

    private void Update()
    {
        UpdateScrollTime();
    }

    /// <summary>
    /// Updates the scroll time for background and floor materials if allowed.
    /// </summary>
    private void UpdateScrollTime()
    {
        if (canAddScrollTime)
        {
            scrollTime += Time.deltaTime;
            floorSR.material.SetFloat(scrollTimeKey, scrollTime);
            skySR.material.SetFloat(scrollTimeKey, scrollTime);
        }
    }

    /// <summary>
    /// Enables or disables scrolling based on the game phase.
    /// </summary>
    private void OnGamePhaseChangedListener(EGamePhase gamePhase)
    {
        canAddScrollTime = gamePhase != EGamePhase.GameOver;
    }

    /// <summary>
    /// Resets the scroll time when the game restarts.
    /// </summary>
    private void OnRestartGameListener()
    {
        scrollTime = 0;
    }

}
