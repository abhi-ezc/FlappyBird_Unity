using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player input and invokes events for gameplay actions (e.g., flap).
/// Implements the Singleton pattern.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInputActions inputActions;
    public UnityEvent onFlap;

    /// <summary>
    /// Ensures only one instance of InputManager exists.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets up input actions and event bindings when enabled.
    /// </summary>
    private void OnEnable()
    {
        onFlap = new UnityEvent();
        inputActions = new PlayerInputActions();
        inputActions.Enable();
        BindEvents();
    }
    /// <summary>
    /// Disables input actions and unbinds events when disabled.
    /// </summary>
    private void OnDisable()
    {
        inputActions.Disable();
        UnBindEvents();
    }

    /// <summary>
    /// Binds input events to their handlers.
    /// </summary>
    private void BindEvents()
    {
        inputActions.Player.Flap.performed += Flap_performed;
    }

    /// <summary>
    /// Unbinds input events from their handlers.
    /// </summary>
    private void UnBindEvents()
    {
        inputActions.Player.Flap.performed -= Flap_performed;
    }

    /// <summary>
    /// Invokes the onFlap event when the flap input is performed during gameplay.
    /// </summary>
    private void Flap_performed(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.gamePhase == EGamePhase.GamePlay)
        {
            onFlap?.Invoke();
        }
    }
}
