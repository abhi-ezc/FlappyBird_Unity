using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer floorSR;
    [SerializeField] private SpriteRenderer skySR;
    private string scrollKey = "_CanScroll";

    private void Start()
    {
        SetCanScroll(1);
    }

    private void OnEnable()
    {
        GameManager.Instance.onGameOver.AddListener(OnGameOverHandler);
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameOver.RemoveListener(OnGameOverHandler);
    }

    private void OnGameOverHandler()
    {
        SetCanScroll(0);
    }

    private void SetCanScroll(float canScroll)
    {
        floorSR.material.SetFloat(scrollKey, canScroll);
        skySR.material.SetFloat(scrollKey, canScroll);
    }
}
