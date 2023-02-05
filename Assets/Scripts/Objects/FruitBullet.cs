using UnityEngine;

public class FruitBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 5;
    private Rigidbody2D _rb;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    public void Shoot(Quaternion rotation, bool isTop = true)
    {
        transform.rotation = rotation;

        Vector3 dir = (isTop) ? -transform.up : transform.right;
        _rb.AddForce(-transform.up * _bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //if (!other.collider.CompareTag("Enemy")) return;
        //print(other.collider.name + " -> " +  other.collider.tag ); 
        AudioManager.GetInstance.PlaySound(AudioManager.AudioList.ShootImpact);
        if (other.collider.TryGetComponent(out SimpleEnemy enemy))
        {
            enemy.Die();
        }

        Destroy(gameObject);
    }
}
