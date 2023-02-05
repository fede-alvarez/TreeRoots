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

    [Header("Animations")]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _animator;

    private float _movement;
    private bool _desiredJump = false;
    private bool _isGrounded = false;
    
    private Vector3 _rayOffset = new Vector3(0, 0.32f);
    private Vector3 _raySize = new Vector3(0.3f, 0.1f);

    private Vector3 _rayLineOffset = new Vector3(0, 0.2f);

    private Rigidbody2D _rb;
    private bool _disableMovement = false;

    private PlayerInput _input;
    private PlayerShoot _shoot;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
        _shoot = GetComponent<PlayerShoot>();
    }

    private void Update() 
    {
        GroundCheck();
        SlopeCheck();

        _movement = _input.GetMovement;

        // Flip sprite
        if (_movement > 0)
            _renderer.flipX = false;
        else if (_movement < 0)
             _renderer.flipX = true;

        _animator.SetBool("Walking", (_rb.velocity.magnitude > 0.1f) ? true : false);
        
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
                _rb.velocity = new Vector2(_rb.velocity.x - (hit.normal.x * 0.1f), _rb.velocity.y);

                Vector3 pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(_rb.velocity.x) * Time.deltaTime * (_rb.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = pos;
            }
        }
    }

    public void DisablePhysics(Transform parent)
    {
        transform.SetParent(parent);
        this.enabled = false;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rb.velocity = Vector2.zero;
    }

    public void EnablePhysics(Transform parent)
    {
        transform.SetParent(parent);
        this.enabled = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
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

    public bool IsTopPlayer => _type == PlayerType.TopTree;
}
