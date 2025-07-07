using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button RestartButton;
    [SerializeField] private Transform CurrentScoreParent;
    [SerializeField] private Transform HighScoreParent;

    private int currentScore = 0;
    private int highScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RestartButton.onClick.AddListener(OnRestartClick);
    }

    private void OnEnable()
    {
        currentScore = GameManager.Instance.GetCurrentScore();
        UpdateCurrentScore();
    }

    private void OnDisable()
    {
        ScoreImagePoolManager.Instance.ReturnItemsToPool(CurrentScoreParent.GetComponentsInChildren<Image>().ToList());
        ScoreImagePoolManager.Instance.ReturnItemsToPool(HighScoreParent.GetComponentsInChildren<Image>().ToList());
    }

    private void OnRestartClick()
    {
        GameManager.Instance.RestartGame();
    }

    private void UpdateCurrentScore()
    {
        List<Image> csImages = ScoreImagePoolManager.Instance.ConvertNumberToImage(currentScore, CurrentScoreParent);
    }
}
