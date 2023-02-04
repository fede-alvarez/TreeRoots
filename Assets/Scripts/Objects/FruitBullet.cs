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
}
