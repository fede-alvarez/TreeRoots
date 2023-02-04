using UnityEngine;
using DG.Tweening;

public class WinUI : MonoBehaviour
{
    private CanvasGroup _group;

    private void Awake() 
    {
        _group = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        EventManager.GameWin += OnWin;
    }

    private void OnWin()
    {
        _group.DOFade(1, 2.0f).OnComplete(() => 
        {
            print("Back to Menu");
        });
    }

    private void OnDestroy() 
    {
        EventManager.GameWin -= OnWin;
    }
}
