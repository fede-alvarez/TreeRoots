using System.Collections;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private readonly int attackPower = 1;

    private readonly float attackRate = 2.0f;

    private readonly float speed = .5f;

    private bool isAttacking = false;

    private Tree tree;

    private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        render.flipX = (transform.position.x > 0) ? true : false;
    }

    public void ConfigureFor(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        rigidBody.velocity = new Vector2(position.x < 0 ? speed : -speed, 0);
    }

    public void Die()
    {
        CancelInvoke("Attack");
        EventManager.OnEnemyDied(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Tree") || isAttacking) return;
        if (other.TryGetComponent(out Tree tree))
        {
            this.tree = tree;
            rigidBody.velocity = Vector2.zero;
            _animator.SetBool("Attack", true);
            InvokeRepeating("Attack", 0, attackRate);
            isAttacking = true;
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(4);
        EventManager.OnEnemyDied (this);
    }

    private void Attack()
    {
        tree.Damage(attackPower);
        EventManager.OnTreeDamaged(tree.GetHealth);
    }
}
