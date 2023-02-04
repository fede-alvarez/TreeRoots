using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType {
        TopTree,
        BottomTree
    }

    [SerializeField] private PlayerType _type = PlayerType.TopTree;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpForce = 5;

    private float _movement;
    private bool _desiredJump = false;
    private bool _isGrounded = false;
    
    private Vector3 _rayOffset = new Vector3(0, 0.32f);
    private Vector3 _raySize = new Vector3(0.3f, 0.1f);

    private Rigidbody2D _rb;
    private bool _disableMovement = false;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        // Check if grounded
        _isGrounded = Physics2D.OverlapBox(transform.position - _rayOffset, _raySize, 0);

        if (!_disableMovement)
            _movement = Input.GetAxis("Horizontal");

        // Jump Handling
        bool jumpPressed = Input.GetButtonDown("Jump");

        if (jumpPressed && _isGrounded)
            _desiredJump = true;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movement * _speed, _rb.velocity.y);

        if (_desiredJump)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _desiredJump = false;
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - _rayOffset, _raySize);
    }

    public bool DisableMovement 
    {
        set { _disableMovement = value; }
    }
}
