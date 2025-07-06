using Unity.VisualScripting;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer floorSR;
    [SerializeField] private SpriteRenderer skySR;

    private float scrollTime;
    private const string scrollTimeKey = "_ScrollTime";

    private void Start()
    {
    }

    private void OnEnable()
    {
        scrollTime = 0;
        GameManager.Instance.onGameOver.AddListener(OnGameOverHandler);
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameOver.RemoveListener(OnGameOverHandler);
    }

    private void OnGameOverHandler()
    {
        this.enabled = false;
    }

    private void Update()
    {
        UpdateScrollTime();
    }

    private void UpdateScrollTime()
    {
        scrollTime += Time.deltaTime;
        floorSR.material.SetFloat(scrollTimeKey, scrollTime);
        skySR.material.SetFloat(scrollTimeKey, scrollTime);
    }


}
