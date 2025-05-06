using UnityEngine;

public class PipeController : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, Time.fixedDeltaTime);
            if (Mathf.Approximately(transform.localPosition.x, 0f))
            {
                PipePoolManager.Instance.ReturnItemToPool(gameObject);
            }
        }
    }
}
