using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInputActions inputActions;
    public UnityEvent onFlap;

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

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        onFlap = new UnityEvent();
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        BindEvents();
    }
    private void OnDisable()
    {
        inputActions.Disable();
        UnBindEvents();
    }

    private void BindEvents()
    {
        inputActions.Player.Flap.performed += Flap_performed;
    }

    private void UnBindEvents()
    {
        inputActions.Player.Flap.performed -= Flap_performed;
    }

    private void Flap_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            onFlap?.Invoke();
        }
    }
}
