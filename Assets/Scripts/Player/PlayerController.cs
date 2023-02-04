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

    private Vector3 _rayLineOffset = new Vector3(0, 0.2f);

    private Rigidbody2D _rb;
    private bool _disableMovement = false;

    private PlayerInput _input;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
    }

    private void Update() 
    {
        GroundCheck();
        SlopeCheck();
        _movement = _input.GetMovement;

        // Jump Handling
        bool jumpPressed = _input.GetJump;

        if (jumpPressed && _isGrounded)
            _desiredJump = true;
    }

    private void FixedUpdate()
    {
        if (_disableMovement) 
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        _rb.velocity = new Vector2(_movement * _speed, _rb.velocity.y);

        if (_desiredJump)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _desiredJump = false;
        }
    }

    private void GroundCheck()
    {
        // Check if grounded
        _isGrounded = Physics2D.OverlapBox(transform.position - _rayOffset, _raySize, 0);
    }

    private void SlopeCheck()
    {
        Debug.DrawRay(transform.position - _rayLineOffset, -transform.up * 0.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position - _rayLineOffset,  -transform.up, 0.2f);

        if (hit.collider != null)
        {
            if (Mathf.Abs(hit.normal.x) > 0.1f)//(hit.normal.y < 0.9f && _rb.velocity.magnitude < 1)
            {
                _rb.velocity = new Vector2(_rb.velocity.x - (hit.normal.x * 0.5f), _rb.velocity.y);
            }
            //print("Slope");
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

    public PlayerType GetPlayerType => _type;
}
