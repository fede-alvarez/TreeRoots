using UnityEngine;
using DG.Tweening;

public class FruitTrigger : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;

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
}
