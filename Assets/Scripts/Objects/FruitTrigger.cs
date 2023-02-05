using UnityEngine;
using DG.Tweening;

public class FruitTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private int _waterDropIndex;
    [SerializeField] private Transform _waterDropParent;

    private bool _hasFruit = false;
    private FruitResource _fruit;
    private PlayerController _player;

    private void Update() 
    {
        if (!_hasFruit) return;

        if (_player != null && _player.InteractionPressed)
        {
            _group.DOFade(0, 1.0f);

            EventManager.OnFruitCollected();
            RemoveFruit();

            _player.CanShoot = true;
        }    
    }

    
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

        //SetDropState(false);
    }

    public void RemoveFruit()
    {
        _fruit.Remove();

        SetDropState(true);

        _fruit = null;
        _hasFruit = false;
    }

    private void SetDropState(bool state)
    {
        _waterDropParent.GetChild(_waterDropIndex).gameObject.SetActive(state);
    }
}
