using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TreeBarUI : MonoBehaviour
{
    private Image _bar;

    private void Awake() 
    {
        _bar = GetComponent<Image>();    
    }

    private void Start() 
    {
        EventManager.TreeDamaged += OnTreeDamaged;    
    }

    private void OnTreeDamaged(int health)
    {
        _bar.DOFillAmount(health / 100.0f, 1.0f);
    }

    private void OnDestroy() 
    {
        EventManager.TreeDamaged -= OnTreeDamaged;
    }
}
