using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float flapPower = 4f;
    [SerializeField] private float rotLerpSpeed = 10f;
    [SerializeField] private LayerMask gameOverLayerMasks;
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (GameManager.Instance.gamePhase  == EGamePhase.GamePlay) 
        {
            UpdateRotation();
        }
        
    }

    private void OnEnable()
    {
        rigidbody2D.simulated = true;
        InputManager.Instance.onFlap.AddListener(OnFlapListener);
        GameManager.Instance.onGameOver.AddListener(OnGameOverListener);
    }

    private void OnDisable()
    {
        rigidbody2D.simulated= false;
        InputManager.Instance.onFlap.RemoveListener(OnFlapListener);
        GameManager.Instance.onGameOver.RemoveListener(OnGameOverListener);
    }

    private void OnFlapListener()
    {
        rigidbody2D.AddForce(Vector2.up * (flapPower - rigidbody2D.linearVelocityY), ForceMode2D.Impulse);
    }

    void UpdateRotation()
    {
        float angle = (Mathf.Atan2(rigidbody2D.linearVelocityY, rigidbody2D.linearVelocityX) * 180) / Mathf.PI;
        Quaternion newRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotLerpSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(( gameOverLayerMasks & 1 << collision.gameObject.layer) !=0)
        {
            GameManager.Instance.SetGameOver();
        }
    }

    private void OnGameOverListener()
    {
        animator.enabled = false;
        rigidbody2D.simulated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.OnTriggerScore();
    }
}
