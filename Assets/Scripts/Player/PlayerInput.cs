using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    [SerializeField] private KeyCode _jumpKey = KeyCode.W;
    [SerializeField] private KeyCode _actionKey = KeyCode.E;

    private PlayerController _controller;

    private float _playerMovement;
    private bool _playerJump;
    private bool _playerInteraction;

    private void Awake() 
    {
        _controller = GetComponent<PlayerController>();    
    }

    private void Update() 
    {
        bool isTopPlayer = _controller.GetPlayerType == PlayerController.PlayerType.TopTree;
        string axis = (isTopPlayer) ? "Horizontal" : "Horizontal2";
        _playerMovement = Input.GetAxis(axis);

        _playerJump = Input.GetKeyDown(_jumpKey);
        _playerInteraction = Input.GetKeyDown(_actionKey);
    }

    public float GetMovement => _playerMovement;
    public bool GetJump => _playerJump;
    public bool GetInteraction => _playerInteraction;
}
