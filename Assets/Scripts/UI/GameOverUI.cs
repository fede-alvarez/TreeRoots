using UnityEngine;
using DG.Tweening;

public class GameOverUI : MonoBehaviour
{
    private CanvasGroup _group;

    private void Awake() 
    {
        _group = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        EventManager.GameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        _group.DOFade(1, 2.0f).OnComplete(() => 
        {
            print("Back to Menu");
        });
    }

    private void OnDestroy() 
    {
        EventManager.GameOver -= OnGameOver;
    }
}
