using UnityEngine;
using DG.Tweening;

public class FruitTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    private bool _hasFruit = false;
    private FruitResource _fruit;
    private PlayerController _player;

    private void Start() 
    {
        _player = GameManager.GetInstance.GetPlayer;    
    }

    private void Update() 
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;
        _group.DOFade(1, 1.0f);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) return;
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
