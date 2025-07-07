using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float flapPower = 4f;
    [SerializeField] private float rotLerpSpeed = 10f;
    [SerializeField] private LayerMask gameOverLayerMasks;
    [SerializeField] private Animator animator;
    private Vector3 SpawnLocation;
    private Quaternion SpawnRotation;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void OnEnable()
    {
        SpawnLocation = transform.position;
        SpawnRotation = transform.rotation;
        InputManager.Instance.onFlap.AddListener(OnFlapListener);
        GameManager.Instance.onGamePhaseChanged.AddListener(OnGamePhaseChangedListener);
        GameManager.Instance.onRestartGame.AddListener(OnRestartGameListener);
    }

    private void OnDisable()
    {
        rigidbody2D.simulated = false;
        InputManager.Instance.onFlap.RemoveListener(OnFlapListener);
        GameManager.Instance.onGamePhaseChanged.RemoveListener(OnGamePhaseChangedListener);
        GameManager.Instance.onRestartGame.RemoveListener(OnRestartGameListener);
    }

    private void OnFlapListener()
    {
        rigidbody2D.AddForce(Vector2.up * (flapPower - rigidbody2D.linearVelocityY), ForceMode2D.Impulse);
    }

    void UpdateRotation()
    {
        if (!rigidbody2D.simulated) return;
        float angle = (Mathf.Atan2(rigidbody2D.linearVelocityY, rigidbody2D.linearVelocityX) * 180) / Mathf.PI;
        Quaternion newRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * rotLerpSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((gameOverLayerMasks & 1 << collision.gameObject.layer) != 0)
        {
            GameManager.Instance.SetGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.OnTriggerScore();
    }

    private void OnRestartGameListener()
    {
        Debug.Log(SpawnLocation);
        rigidbody2D.linearVelocity = Vector2.zero;
        transform.position = SpawnLocation;
        transform.rotation = SpawnRotation;
    }

    private void OnGamePhaseChangedListener(EGamePhase gamePhase)
    {
        Debug.Log($"Game Phase Changed: {gamePhase}");
        switch (gamePhase)
        {
            case EGamePhase.MainMenu:
            case EGamePhase.GetReady:
                animator.enabled = true;
                rigidbody2D.simulated = false;
                break;

            case EGamePhase.GameOver:
                animator.enabled = false;
                rigidbody2D.simulated = false;
                break;

            case EGamePhase.GamePlay:
                animator.enabled = true;
                rigidbody2D.simulated = true;
                break;
        }
    }
}
