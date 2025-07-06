using UnityEngine;
using System.Collections.Generic;
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

    void Start()
    {
        WarmScoreImagePool();
    }

    private void OnEnable()
    {
        GameManager.Instance.onScoreChanged.AddListener(OnScoreChangedListener);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            scoreImage.SetActive(true);
            return scoreImage;
        }
        else
        {
            Debug.LogWarning("Score image pool is empty. Consider increasing pool size.");
            return null;
        }
    }

    private void OnScoreChangedListener(int score)
    {
        int numberOfDigits = (int)Mathf.Floor(Mathf.Log10(score * 1)) + 1;
        bool isRequiredNew = (numberOfDigits - scoreImageParent.childCount) > 0;
        if (isRequiredNew)
        {
            GameObject go = GetItemFromPool();
            Image image = go.GetComponent<Image>();
            go.transform.SetParent(scoreImageParent, false);
            go.SetActive(true);
        }

        UtilityFunctions.CovertNumbersToImage(score,countSpriteImages,scoreImageParent.GetComponentsInChildren<Image>());
    }
}
