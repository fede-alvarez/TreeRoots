using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _controller;
    private float _playerMovement;
    private bool _playerJump;
    private bool _playerInteract;
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
        _playerJump = true;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        _playerInteract = true;
    }

    private void Update() 
    {
        _playerMovement = _movement.x;
    }

    public float GetMovement => _playerMovement;
    public bool GetJump => _playerJump;
    public bool GetInteract => _playerInteract;

    public void ResetInteract()
    {
        _playerInteract = false;
    }

    public void ResetJump()
    {
        _playerJump = false;
    }
}
