using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _controller;

    private float _playerMovement;
    private bool _playerJump;
    private bool _playerInteraction;
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
        _playerJump = context.action.triggered;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        _playerInteraction = context.action.triggered;
    }

    private void Update() 
    {
        _playerMovement = _movement.x;
    }

    public float GetMovement => _playerMovement;
    public bool GetJump => _playerJump;
    public bool GetInteraction => _playerInteraction;
}
