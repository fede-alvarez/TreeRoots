using UnityEngine;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform _logo;

    private void Start() 
    {
        _logo.DOScale(45, 2.0f).SetLoops(-1, LoopType.Yoyo);    
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
