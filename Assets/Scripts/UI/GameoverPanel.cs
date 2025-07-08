using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Controls the Game Over UI panel, including score display and restart button.
/// </summary>
public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button RestartButton;
    [SerializeField] private Transform CurrentScoreParent;
    [SerializeField] private Transform HighScoreParent;

    private int currentScore = 0;

    /// <summary>
    /// Registers the restart button click event.
    /// </summary>
    void Start()
    {
        RestartButton.onClick.AddListener(OnRestartClick);
    }

    /// <summary>
    /// Updates the current score display when the panel is enabled.
    /// </summary>
    private void OnEnable()
    {
        currentScore = GameManager.Instance.GetCurrentScore();
        UpdateCurrentScore();
        UpdateHighScore();
    }

    /// <summary>
    /// Returns score images to the pool when the panel is disabled.
    /// </summary>
    private void OnDisable()
    {
        ScoreImagePoolManager.Instance.ReturnItemsToPool(CurrentScoreParent.GetComponentsInChildren<Image>().ToList());
        ScoreImagePoolManager.Instance.ReturnItemsToPool(HighScoreParent.GetComponentsInChildren<Image>().ToList());
    }

    /// <summary>
    /// Handles the restart button click event.
    /// </summary>
    private void OnRestartClick()
    {
        GameManager.Instance.RestartGame();
    }

    /// <summary>
    /// Updates the current score UI images.
    /// </summary>
    private void UpdateCurrentScore()
    {
        List<Image> csImages = ScoreImagePoolManager.Instance.ConvertNumberToImage(currentScore, CurrentScoreParent);
    }

    private void UpdateHighScore()
    {
        List<Image> hsImages = ScoreImagePoolManager.Instance.ConvertNumberToImage(GameManager.Instance.GetHighScore(), HighScoreParent);
    }
}
