using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the gameplay UI panel, including score display and game over handling.
/// </summary>
public class GameplayPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject scoreImagePrefab; // Prefab for the score image
    [SerializeField] private Transform scoreImageParent; // Parent transform for the score images

    /// <summary>
    /// Registers for score and game over events when enabled.
    /// </summary>
    private void OnEnable()
    {
        GameManager.Instance.onScoreChanged.AddListener(OnScoreChangedListener);
        GameManager.Instance.onGameOver.AddListener(OnGameOverListener);
        UpdateScoreImages(GameManager.Instance.GetCurrentScore());
    }

    /// <summary>
    /// Unregisters from events when disabled.
    /// </summary>
    private void OnDisable()
    {
        GameManager.Instance.onScoreChanged.RemoveListener(OnScoreChangedListener);
        GameManager.Instance.onGameOver.RemoveListener(OnGameOverListener);
    }

    /// <summary>
    /// Updates the score display when the score changes.
    /// </summary>
    private void OnScoreChangedListener(int score)
    {
        UpdateScoreImages(score);
    }

    /// <summary>
    /// Updates the score images to reflect the current score.
    /// </summary>
    private void UpdateScoreImages(int score)
    {
        int numberOfDigits = score > 0 ? ((int)Mathf.Floor(Mathf.Log10(score * 1)) + 1) : 1;
        bool isRequiredNew = (numberOfDigits - scoreImageParent.childCount) > 0;
        if (isRequiredNew)
        {
            Image image = ScoreImagePoolManager.Instance.GetItemFromPool();
            if (!image)
            {
                Debug.LogError("Score image pool is giving null");
                return;
            }

            image.gameObject.transform.SetParent(scoreImageParent, false);
            image.gameObject.SetActive(true);
        }
        ScoreImagePoolManager.Instance.UpdateNumberToImage(score, scoreImageParent.GetComponentsInChildren<Image>().ToList());
    }

    /// <summary>
    /// Returns score images to the pool when the game is over.
    /// </summary>
    private void OnGameOverListener()
    {
        ScoreImagePoolManager.Instance.ReturnItemsToPool(scoreImageParent.GetComponentsInChildren<Image>().ToList());
    }
}
