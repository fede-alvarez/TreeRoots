using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _controller;
    private float _playerMovement;
    private Vector2 _movement = Vector2.zero;

    private void Awake() 
    {
        _controller = GetComponent<PlayerController>();    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        EventManager.OnPlayerJumped();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        EventManager.OnPlayerInteracted();
    }

    private void Update() 
    {
        _playerMovement = _movement.x;
    }

    public float GetMovement => _playerMovement;
}
