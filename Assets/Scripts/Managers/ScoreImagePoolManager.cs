using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreImagePoolManager : MonoBehaviour
{
    [SerializeField] private PoolManagerSO poolManagerConfig;
    [SerializeField] private List<Sprite> digitImageSprites;

    private PoolManager<Image> poolManager = new PoolManager<Image>();
    public static ScoreImagePoolManager Instance { get; private set; }

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
        poolManager.Initialize(poolManagerConfig.poolSize, poolManagerConfig.objectPrefab, poolManagerConfig.isExpandable, transform);
    }

    private void Start()
    {
        poolManager.WarmPool();
    }

    public Image GetItemFromPool()
    {
        return poolManager.Get();
    }

    public void ReturnItemsToPool(List<Image> images)
    {
        poolManager.ReturnItems(images);
    }

    public List<Image> ConvertNumberToImage(int num, Transform parent = null)
    {
        int numberOfDigits = UtilityFunctions.GetNumberOfDigits(num);
        List<Image> images = poolManager.GetN(numberOfDigits, true);
        UtilityFunctions.CovertNumbersToImage(num, digitImageSprites, images, parent);
        return images;
    }

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
