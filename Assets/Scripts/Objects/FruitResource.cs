using UnityEngine;
using DG.Tweening;

public class FruitResource : MonoBehaviour
{
    private CircleCollider2D _collider;
    private void Awake() 
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start() 
    {
        transform.DOScale(new Vector3(1.1f, 1.1f), 1.5f).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0,0,5), 0.8f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Remove()
    {
        _collider.enabled = false;
        
        transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => {
            Destroy(gameObject);
        });
    }
}
