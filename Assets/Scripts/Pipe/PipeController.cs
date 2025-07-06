using UnityEngine;

public class PipeController : MonoBehaviour
{

    private Vector3 endLoc;
    private void OnEnable()
    {
        endLoc = new Vector3(0, transform.localPosition.y,0);
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endLoc, Time.fixedDeltaTime);
            if (Mathf.Approximately(transform.localPosition.x, 0f))
            {
                PipePoolManager.Instance.ReturnItemToPool(gameObject);
            }
        }
    }
}
