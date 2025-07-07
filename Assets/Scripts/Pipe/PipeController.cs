using UnityEngine;

/// <summary>
/// Controls the behavior of a single pipe in the game.
/// Handles movement and pooling logic.
/// </summary>
public class PipeController : MonoBehaviour
{
    private Vector3 endLoc;

    /// <summary>
    /// Registers for restart events and sets the end location when enabled.
    /// </summary>
    private void OnEnable()
    {
        endLoc = new Vector3(0, transform.localPosition.y, 0);
        GameManager.Instance.onRestartGame.AddListener(OnRestartGameListener);
    }

    /// <summary>
    /// Unregisters from restart events when disabled.
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.onRestartGame.RemoveListener(OnRestartGameListener);
    }

    /// <summary>
    /// Moves the pipe towards the end location during gameplay. Returns to pool when off-screen.
    /// </summary>
    void FixedUpdate()
    {
        if (GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endLoc, Time.fixedDeltaTime);
            if (Mathf.Approximately(transform.localPosition.x, 0f))
            {
                PipePoolManager.Instance.ReturnItemToPool(this);
            }
        }
    }

    /// <summary>
    /// Returns the pipe to the pool when the game restarts.
    /// </summary>
    private void OnRestartGameListener()
    {
        PipePoolManager.Instance.ReturnItemToPool(this);
    }
}
