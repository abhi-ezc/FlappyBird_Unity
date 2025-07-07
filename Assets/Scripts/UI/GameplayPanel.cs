using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject scoreImagePrefab; // Prefab for the score image
    [SerializeField] private Transform scoreImageParent; // Parent transform for the score images

    [Header("Score Images")]
    [Tooltip("Array to hold the score images. 0th index should be for 0 sprite, 1st index for 1 sprite, etc.")]
    [SerializeField] private Sprite[] countSpriteImages; // Array to hold the score images

    private const int poolSize = 5; // Size of the pool
    private Queue<GameObject> scoreImagePool = new Queue<GameObject>(); // Pool for score images
    private List<GameObject> scoreImagesInUse = new List<GameObject>();

    void Start()
    {
    }

    private void OnEnable()
    {
        WarmScoreImagePool();
        GameManager.Instance.onScoreChanged.AddListener(OnScoreChangedListener);
        GameManager.Instance.onGameOver.AddListener(OnGameOverListener);
        UpdateScoreImages(GameManager.Instance.GetCurrentScore());
    }

    private void OnDisable()
    {
        GameManager.Instance.onScoreChanged.RemoveListener(OnScoreChangedListener);
        GameManager.Instance.onGameOver.RemoveListener(OnGameOverListener);
    }

    private void WarmScoreImagePool()
    {
        while (scoreImagePool.Count < poolSize)
        {
            GameObject scoreImage = Instantiate(scoreImagePrefab, transform);
            scoreImage.SetActive(false);
            scoreImage.GetComponent<Image>().sprite = countSpriteImages[0]; // Set default sprite to 0
            scoreImagePool.Enqueue(scoreImage);
        }
    }

    private GameObject GetItemFromPool()
    {
        if (scoreImagePool.Count > 0)
        {
            GameObject scoreImage = scoreImagePool.Dequeue();
            scoreImagesInUse.Add(scoreImage);
            scoreImage.SetActive(true);
            return scoreImage;
        }
        else
        {
            Debug.LogWarning("Score image pool is empty. Consider increasing pool size.");
            return null;
        }
    }

    private void ReturnAllItemsToPool()
    {
        foreach(GameObject item in  scoreImagesInUse)
        {
            item.SetActive(false);
            item.transform.parent = transform;
            scoreImagePool.Enqueue(item);
        }

        scoreImagesInUse.Clear();
    }

    private void OnScoreChangedListener(int score)
    {
        UpdateScoreImages(score);
    }

    private void UpdateScoreImages(int score)
    {
        int numberOfDigits = score > 0 ? ((int)Mathf.Floor(Mathf.Log10(score * 1)) + 1): 1;
        bool isRequiredNew = (numberOfDigits - scoreImageParent.childCount) > 0;
        if (isRequiredNew)
        {
            GameObject go = GetItemFromPool();
            if(!go)
            {
                Debug.LogError("Score image pool is giving null");
                return;
            }

            Image image = go.GetComponent<Image>();
            go.transform.SetParent(scoreImageParent, false);
            go.SetActive(true);
        }

        UtilityFunctions.CovertNumbersToImage(score, countSpriteImages, scoreImageParent.GetComponentsInChildren<Image>());
    }

    private void OnGameOverListener()
    {
        ReturnAllItemsToPool();
    }
}
