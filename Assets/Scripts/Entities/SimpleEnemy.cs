using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private int _attackPower = 1;
    private float _attackRate = 1.5f;
    private bool _isAttacking = false;

    private Tree _tree;
    private Rigidbody2D _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        _rb.velocity = new Vector2(-1, 0);
    }

    private void Attack()
    {
        _tree.Damage(_attackPower);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Tree") || _isAttacking) return;
        if (other.TryGetComponent(out Tree tree))
        {
            _tree = tree;
            _rb.velocity = Vector2.zero;
            InvokeRepeating("Attack", 0, _attackRate);
            _isAttacking = true;
        }
    }
}
