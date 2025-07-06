using Unity.VisualScripting;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer floorSR;
    [SerializeField] private SpriteRenderer skySR;

    private float scrollTime;
    private const string scrollTimeKey = "_ScrollTime";
    private bool canAddScrollTime = true;

    private void OnEnable()
    {
        scrollTime = 0;
        GameManager.Instance.onGamePhaseChanged.AddListener(OnGamePhaseChangedListener);
        GameManager.Instance.onRestartGame.AddListener(OnRestartGameListener);
    }

    private void OnDisable()
    {
        GameManager.Instance.onGamePhaseChanged.RemoveListener(OnGamePhaseChangedListener);
        GameManager.Instance.onRestartGame.RemoveListener(OnRestartGameListener);
    }

    private void Update()
    {
        UpdateScrollTime();
    }

    private void UpdateScrollTime()
    {
        if(canAddScrollTime)
        {
            scrollTime += Time.deltaTime;
            floorSR.material.SetFloat(scrollTimeKey, scrollTime);
            skySR.material.SetFloat(scrollTimeKey, scrollTime);
        }
    }

    private void OnGamePhaseChangedListener(EGamePhase gamePhase)
    {
        canAddScrollTime = gamePhase != EGamePhase.GameOver;
    }

    private void OnRestartGameListener()
    {
        scrollTime = 0;
    }

}
