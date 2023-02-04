using System.Collections;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    private readonly int attackPower = 1;

    private readonly float attackRate = 1.5f;

    private readonly float speed = 1f;

    private bool isAttacking = false;

    private Tree tree;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ConfigureFor(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        rigidBody.velocity = new Vector2(position.x < 0 ? speed : -speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Tree") || isAttacking) return;
        if (other.TryGetComponent(out Tree tree))
        {
            this.tree = tree;
            rigidBody.velocity = Vector2.zero;
            InvokeRepeating("Attack", 0, attackRate);
            isAttacking = true;
        }
    }

    private void Attack()
    {
        tree.Damage (attackPower);
    }
}
