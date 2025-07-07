using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages a pool of UI Image objects for displaying score digits efficiently.
/// Implements the Singleton pattern.
/// </summary>
public class ScoreImagePoolManager : MonoBehaviour
{
    [SerializeField] private PoolManagerSO poolManagerConfig;
    [SerializeField] private List<Sprite> digitImageSprites;

    private PoolManager<Image> poolManager = new PoolManager<Image>();
    public static ScoreImagePoolManager Instance { get; private set; }

    /// <summary>
    /// Ensures only one instance of ScoreImagePoolManager exists.
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
    /// Initializes the pool manager with configuration on enable.
    /// </summary>
    private void OnEnable()
    {
        poolManager.Initialize(poolManagerConfig.poolSize, poolManagerConfig.objectPrefab, poolManagerConfig.isExpandable, transform);
    }

    /// <summary>
    /// Warms up the pool at the start of the game.
    /// </summary>
    private void Start()
    {
        poolManager.WarmPool();
    }

    /// <summary>
    /// Retrieves an Image from the pool.
    /// </summary>
    public Image GetItemFromPool()
    {
        return poolManager.Get();
    }

    /// <summary>
    /// Returns a list of Images to the pool.
    /// </summary>
    public void ReturnItemsToPool(List<Image> images)
    {
        poolManager.ReturnItems(images);
    }

    /// <summary>
    /// Converts a number to digit images and returns the list of Images.
    /// </summary>
    public List<Image> ConvertNumberToImage(int num, Transform parent = null)
    {
        int numberOfDigits = UtilityFunctions.GetNumberOfDigits(num);
        List<Image> images = poolManager.GetN(numberOfDigits, true);
        UtilityFunctions.CovertNumbersToImage(num, digitImageSprites, images, parent);
        return images;
    }

    /// <summary>
    /// Updates the digit images for a given number using the provided Images.
    /// </summary>
    public void UpdateNumberToImage(int num, List<Image> images)
    {
        int numberOfDigits = UtilityFunctions.GetNumberOfDigits(num);
        int sizeDiff = numberOfDigits - images.Count;
        if (sizeDiff > 0)
        {
            images.AddRange(ConvertNumberToImage(sizeDiff));
        }

        UtilityFunctions.CovertNumbersToImage(num, digitImageSprites, images);
    }
}
