using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button RestartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RestartButton.onClick.AddListener(OnRestartClick);
    }

    
    private void OnRestartClick()
    {
        GameManager.Instance.RestartGame();
    }
}
