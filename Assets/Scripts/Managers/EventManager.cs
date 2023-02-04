using UnityEngine.Events;

public static class EventManager
{
    #region Fruits Related
    public static event UnityAction FruitCollected;
    public static event UnityAction FruitSpawned;
    public static void OnFruitCollected() => FruitCollected?.Invoke();
    public static void OnFruitSpawned() => FruitSpawned?.Invoke();
    #endregion

    #region General
    public static event UnityAction GameOver;
    public static event UnityAction GameWin;
    public static void OnGameOver() => GameOver?.Invoke();
    public static void OnGameWin() => GameWin?.Invoke();
    #endregion
}