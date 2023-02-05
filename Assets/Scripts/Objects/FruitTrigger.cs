using UnityEngine;
using DG.Tweening;

public class FruitTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    private bool _hasFruit = false;
    private FruitResource _fruit;
    private PlayerController _player;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;
        if (other.TryGetComponent(out PlayerController controller))
            _player = controller;
        
        _group.DOFade(1, 1.0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_player != null) _player = null;

        _group.DOFade(0, 1.0f);
    }

    public void SetFruit(FruitResource fruit)
    {
        _fruit = fruit;
        _hasFruit = true;
    }

    public void RemoveFruit()
    {
        _fruit.Remove();

        _fruit = null;
        _hasFruit = false;
    }
}
