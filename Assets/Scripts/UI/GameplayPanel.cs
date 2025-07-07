using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject scoreImagePrefab; // Prefab for the score image
    [SerializeField] private Transform scoreImageParent; // Parent transform for the score images

    private void OnEnable()
    {
        GameManager.Instance.onScoreChanged.AddListener(OnScoreChangedListener);
        GameManager.Instance.onGameOver.AddListener(OnGameOverListener);
        UpdateScoreImages(GameManager.Instance.GetCurrentScore());
    }

    private void OnDisable()
    {
        GameManager.Instance.onScoreChanged.RemoveListener(OnScoreChangedListener);
        GameManager.Instance.onGameOver.RemoveListener(OnGameOverListener);
    }

    private void OnScoreChangedListener(int score)
    {
        UpdateScoreImages(score);
    }

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

    private void OnGameOverListener()
    {
        ScoreImagePoolManager.Instance.ReturnItemsToPool(scoreImageParent.GetComponentsInChildren<Image>().ToList());
    }
}
